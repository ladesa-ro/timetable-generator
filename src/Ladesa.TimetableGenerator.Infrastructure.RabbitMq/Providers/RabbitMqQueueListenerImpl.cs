using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Providers;

/// <summary>
/// A resilient RabbitMQ queue listener that manages its own channel and
/// automatically re-subscribes on failure and reconnection.
/// </summary>
public sealed class RabbitMqQueueListenerImpl : RabbitMqDisposableBase, IQueueListener
{
    private readonly RabbitMqPersistentConnectionImpl _persistentConnectionImpl;
    private readonly ILogger<RabbitMqQueueListenerImpl> _logger;
    private readonly SemaphoreSlim _initializationSemaphore = new(1, 1);

    private IChannel? _channel;
    private string? _queueName;
    private string? _consumerTag;
    private Func<byte[], Task>? _messageHandler;

    public RabbitMqQueueListenerImpl(
        RabbitMqPersistentConnectionImpl persistentConnectionImpl,
        ILogger<RabbitMqQueueListenerImpl> logger)
    {
        _persistentConnectionImpl = persistentConnectionImpl;
        _logger = logger;

        _persistentConnectionImpl.OnReconnected += OnConnectionImplReconnected;
    }

    public async Task SubscribeAsync(
        string queue,
        Func<byte[], Task> handler,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(queue)) throw new ArgumentNullException(nameof(queue));
        _messageHandler = handler ?? throw new ArgumentNullException(nameof(handler));
        _queueName = queue;

        await _persistentConnectionImpl.TryConnectAsync(cancellationToken);
        if (_persistentConnectionImpl.IsConnected) await InitializeConsumerAsync(cancellationToken);
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

            _channel = await _persistentConnectionImpl.CreateChannelAsync(cancellationToken);
            _channel.CallbackExceptionAsync += OnChannelCallbackException;

            var dlxName = $"dlx.{_queueName}";
            var dlqName = $"dlq.{_queueName}";

            await _channel.ExchangeDeclareAsync(dlxName, ExchangeType.Fanout, cancellationToken: cancellationToken);
            await _channel.QueueDeclareAsync(dlqName, true, false, false, cancellationToken: cancellationToken);
            await _channel.QueueBindAsync(dlqName, dlxName, "", cancellationToken: cancellationToken);

            var args = new Dictionary<string, object> { { "x-dead-letter-exchange", dlxName } };
            await _channel.QueueDeclareAsync(_queueName, true, false, false, args, cancellationToken: cancellationToken);

            await _channel.BasicQosAsync(0, 5, false, cancellationToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += OnMessageReceivedAsync;

            _consumerTag = await _channel.BasicConsumeAsync(_queueName, false, consumer, cancellationToken);
            _logger.LogInformation("Consumer started on queue '{QueueName}' with ConsumerTag '{ConsumerTag}'.", _queueName, _consumerTag);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize consumer for queue '{QueueName}'.", _queueName);
            _channel?.Dispose();
            _channel = null;
        }
        finally
        {
            _initializationSemaphore.Release();
        }
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
            _logger.LogError(ex, "Error processing message from queue '{QueueName}'. Sending to DLQ.", _queueName);
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

        _persistentConnectionImpl.OnReconnected -= OnConnectionImplReconnected;

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
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error disposing listener for queue '{QueueName}'.", _queueName);
        }
        finally
        {
            _initializationSemaphore.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}
