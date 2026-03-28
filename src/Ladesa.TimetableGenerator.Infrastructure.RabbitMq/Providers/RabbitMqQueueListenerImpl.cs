using System.Net.Sockets;
using Ladesa.TimetableGenerator.Application.Todo.Ports;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Config;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Constants;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Providers;

/// <summary>
/// A resilient RabbitMQ queue listener that manages its own channel and
/// automatically re-subscribes on failure and reconnection.
/// </summary>
public sealed class RabbitMqQueueListenerImpl : RabbitMqDisposableBase, IQueueListener
{
    private readonly IRabbitMqPersistentConnection _persistentConnection;
    private readonly ILogger<RabbitMqQueueListenerImpl> _logger;
    private readonly ushort _prefetchCount;
    private readonly SemaphoreSlim _initializationSemaphore = new(1, 1);

    private IChannel? _channel;
    private string? _queueName;
    private string? _consumerTag;
    private Func<byte[], Task>? _messageHandler;

    public RabbitMqQueueListenerImpl(
        IRabbitMqPersistentConnection persistentConnection,
        IRabbitMqConfigProvider configProvider,
        ILogger<RabbitMqQueueListenerImpl> logger)
    {
        _persistentConnection = persistentConnection;
        _logger = logger;
        _prefetchCount = configProvider.GetConnectionOptions().PrefetchCount;

        _persistentConnection.OnReconnected += OnConnectionImplReconnected;
    }

    public async Task SubscribeAsync(
        string queue,
        Func<byte[], Task> handler,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(queue)) throw new ArgumentNullException(nameof(queue));
        _messageHandler = handler ?? throw new ArgumentNullException(nameof(handler));
        _queueName = queue;

        await _persistentConnection.TryConnectAsync(cancellationToken);
        if (_persistentConnection.IsConnected) await InitializeConsumerAsync(cancellationToken);
    }

    private async Task InitializeConsumerAsync(CancellationToken cancellationToken = default)
    {
        await _initializationSemaphore.WaitAsync(cancellationToken);

        try
        {
            if (_channel is { IsOpen: true }) return;

            if (string.IsNullOrEmpty(_queueName) || _messageHandler is null)
            {
                _logger.LogWarning("RabbitMQ Listener: queue name or handler not configured. Cannot start.");
                return;
            }

            _logger.LogInformation("Initializing consumer for queue '{QueueName}'...", _queueName);

            await SetupChannelAsync(cancellationToken);
            await ConfigureDeadLetterQueueAsync(cancellationToken);
            await StartConsumerAsync(cancellationToken);
        }
        catch (BrokerUnreachableException ex)
        {
            _logger.LogError(ex, "Broker unreachable while initializing consumer for queue '{QueueName}'.", _queueName);
            _channel?.Dispose();
            _channel = null;
        }
        catch (AlreadyClosedException ex)
        {
            _logger.LogError(ex, "Channel or connection already closed while initializing consumer for queue '{QueueName}'.", _queueName);
            _channel?.Dispose();
            _channel = null;
        }
        catch (SocketException ex)
        {
            _logger.LogError(ex, "Socket error while initializing consumer for queue '{QueueName}'.", _queueName);
            _channel?.Dispose();
            _channel = null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error initializing consumer for queue '{QueueName}'.", _queueName);
            _channel?.Dispose();
            _channel = null;
        }
        finally
        {
            _initializationSemaphore.Release();
        }
    }

    private async Task SetupChannelAsync(CancellationToken cancellationToken)
    {
        _channel = await _persistentConnection.CreateChannelAsync(cancellationToken);
        _channel.CallbackExceptionAsync += OnChannelCallbackException;
    }

    private async Task ConfigureDeadLetterQueueAsync(CancellationToken cancellationToken)
    {
        var dlxName = RabbitMqNamingConventions.GetDlxName(_queueName!);
        var dlqName = RabbitMqNamingConventions.GetDlqName(_queueName!);

        await _channel!.ExchangeDeclareAsync(dlxName, ExchangeType.Fanout, cancellationToken: cancellationToken);
        await _channel.QueueDeclareAsync(dlqName, true, false, false, cancellationToken: cancellationToken);
        await _channel.QueueBindAsync(dlqName, dlxName, "", cancellationToken: cancellationToken);

        var args = new Dictionary<string, object> { { "x-dead-letter-exchange", dlxName } };
        await _channel.QueueDeclareAsync(_queueName!, true, false, false, args, cancellationToken: cancellationToken);

        await _channel.BasicQosAsync(0, _prefetchCount, false, cancellationToken);
    }

    private async Task StartConsumerAsync(CancellationToken cancellationToken)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel!);
        consumer.ReceivedAsync += OnMessageReceivedAsync;

        _consumerTag = await _channel!.BasicConsumeAsync(_queueName!, false, consumer, cancellationToken);
        _logger.LogInformation("Consumer started on queue '{QueueName}' with ConsumerTag '{ConsumerTag}'.", _queueName, _consumerTag);
    }

    private async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs ea)
    {
        if (_messageHandler is null) return;

        var message = ea.Body.ToArray();
        try
        {
            await _messageHandler(message);
            await _channel.BasicAckAsync(ea.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing message from queue '{QueueName}' (DeliveryTag={DeliveryTag}, Size={MessageSize} bytes). Sending to DLQ.",
                _queueName, ea.DeliveryTag, message.Length);
            await _channel.BasicNackAsync(ea.DeliveryTag, false, false);
        }
    }

    private void OnConnectionImplReconnected()
    {
        if (CheckDisposed()) return;
        _logger.LogInformation("RabbitMQ connection re-established. Restarting consumer...");
        _ = InitializeConsumerAsync();
    }

    private Task OnChannelCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (CheckDisposed()) return Task.CompletedTask;
        _logger.LogWarning(e.Exception, "Channel callback exception. Restarting consumer...");
        _ = InitializeConsumerAsync();
        return Task.CompletedTask;
    }

    public override async ValueTask DisposeAsync()
    {
        if (!TryMarkDisposed()) return;

        _persistentConnection.OnReconnected -= OnConnectionImplReconnected;

        try
        {
            if (_channel is not null && _consumerTag is not null)
                await _channel.BasicCancelAsync(_consumerTag);
            if (_channel is not null)
            {
                _channel.CallbackExceptionAsync -= OnChannelCallbackException;
                _channel.Dispose();
                await _channel.CloseAsync();
            }

            _logger.LogInformation("Listener for queue '{QueueName}' disposed successfully.", _queueName);
        }
        catch (AlreadyClosedException ex)
        {
            _logger.LogWarning(ex, "Channel already closed during disposal of listener for queue '{QueueName}'.", _queueName);
        }
        catch (IOException ex)
        {
            _logger.LogWarning(ex, "I/O error during disposal of listener for queue '{QueueName}'.", _queueName);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Unexpected error disposing listener for queue '{QueueName}'.", _queueName);
        }
        finally
        {
            _initializationSemaphore.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}
