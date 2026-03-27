using System.Net.Sockets;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Providers;

/// <summary>
/// Resilient RabbitMQ message publisher that manages and reuses its own channel,
/// ensures queue existence and publishes persistent messages with retry policy.
/// </summary>
public sealed class RabbitMqQueuePublisherImpl : RabbitMqDisposableBase, IQueuePublisher
{
    private readonly RabbitMqPersistentConnectionImpl _persistentConnectionImpl;
    private readonly ILogger<RabbitMqQueuePublisherImpl> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly SemaphoreSlim _channelSemaphore = new(1, 1);

    private IChannel? _channel;

    public RabbitMqQueuePublisherImpl(
        RabbitMqPersistentConnectionImpl persistentConnectionImpl,
        ILogger<RabbitMqQueuePublisherImpl> logger,
        int retryCount = 3)
    {
        _persistentConnectionImpl = persistentConnectionImpl;
        _logger = logger;

        _persistentConnectionImpl.OnReconnected += OnConnectionImplReconnected;

        _retryPolicy = Policy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .Or<IOException>()
            .Or<AlreadyClosedException>()
            .WaitAndRetryAsync(
                retryCount,
                retryAttempt => TimeSpan.FromSeconds(retryAttempt),
                (ex, time, attempt, context) =>
                {
                    _logger.LogWarning(ex,
                        "Failed to publish message. Retrying in {Time}s. Attempt {Attempt}/{RetryCount}",
                        time.TotalSeconds, attempt, retryCount);
                }
            );
    }

    /// <summary>
    /// Publishes a message to a specific queue asynchronously with resilience.
    /// </summary>
    public async Task PublishAsync(string queue, byte[] body, CancellationToken cancellationToken = default)
    {
        var channel = await GetOrCreateChannelAsync(queue, cancellationToken);

        var properties = new BasicProperties { Persistent = true };

        _logger.LogInformation("Publishing message to queue '{QueueName}' ({Size} bytes)...", queue, body.Length);

        await _retryPolicy.ExecuteAsync(async (ct) =>
        {
            await channel.BasicPublishAsync(
                string.Empty,
                queue,
                true,
                body: body,
                basicProperties: properties,
                cancellationToken: ct
            );
        }, cancellationToken);

        _logger.LogInformation("Message published successfully to queue '{QueueName}'.", queue);
    }

    private async Task<IChannel> GetOrCreateChannelAsync(string queueName, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();

        await _channelSemaphore.WaitAsync(cancellationToken);
        try
        {
            if (_channel is { IsOpen: true }) return _channel;

            _logger.LogInformation("Channel does not exist or is closed. Creating a new channel...");

            _channel = await _persistentConnectionImpl.CreateChannelAsync(cancellationToken);
            _channel.CallbackExceptionAsync += OnChannelCallbackException;

            await _channel.QueueDeclareAsync(queueName, true, false, false, null, cancellationToken: cancellationToken);

            _logger.LogInformation("Channel created and queue '{QueueName}' declared successfully.", queueName);

            return _channel;
        }
        finally
        {
            _channelSemaphore.Release();
        }
    }

    private void OnConnectionImplReconnected()
    {
        if (CheckDisposed()) return;
        _logger.LogInformation("RabbitMQ connection re-established. Clearing channel for recreation on next publish.");
        _channel?.Dispose();
        _channel = null;
    }

    private Task OnChannelCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (CheckDisposed()) return Task.CompletedTask;
        _logger.LogWarning(e.Exception, "Channel callback exception. Channel will be recreated on next publish.");
        _channel?.Dispose();
        _channel = null;
        return Task.CompletedTask;
    }

    public override async ValueTask DisposeAsync()
    {
        if (!TryMarkDisposed()) return;

        _persistentConnectionImpl.OnReconnected -= OnConnectionImplReconnected;

        try
        {
            if (_channel is not null)
            {
                _channel.CallbackExceptionAsync -= OnChannelCallbackException;
                _channel.Dispose();
            }

            _logger.LogInformation("Publisher disposed successfully.");
        }
        catch (AlreadyClosedException ex)
        {
            _logger.LogWarning(ex, "Channel already closed during publisher disposal.");
        }
        catch (IOException ex)
        {
            _logger.LogWarning(ex, "I/O error during publisher disposal.");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Unexpected error disposing publisher.");
        }
        finally
        {
            _channelSemaphore.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}
