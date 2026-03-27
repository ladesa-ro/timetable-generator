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
public sealed class RabbitMqQueuePublisherImpl : IQueuePublisher, IAsyncDisposable
{
    private readonly RabbitMqPersistentConnectionImpl _persistentConnectionImpl;
    private readonly ILogger<RabbitMqQueuePublisherImpl> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly SemaphoreSlim _channelSemaphore = new(1, 1);

    private IChannel? _channel;
    private bool _disposed;

    public RabbitMqQueuePublisherImpl(
        RabbitMqPersistentConnectionImpl persistentConnectionImpl,
        ILogger<RabbitMqQueuePublisherImpl> logger,
        int retryCount = 3)
    {
        _persistentConnectionImpl = persistentConnectionImpl;
        _logger = logger;

        // Assina o evento para saber quando recriar o canal
        _persistentConnectionImpl.OnReconnected += OnConnectionImplReconnected;

        // Retry policy for publish operations
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
                        "Falha ao publicar mensagem. Retentando em {Time}s. Tentativa {Attempt}/{RetryCount}",
                        time.TotalSeconds, attempt, retryCount);
                }
            );
    }

    /// <summary>
    /// Publishes a message to a specific queue asynchronously with resilience.
    /// </summary>
    /// <param name="queue">O nome da fila.</param>
    /// <param name="body">O corpo da mensagem em bytes.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    public async Task PublishAsync(string queue, byte[] body, CancellationToken cancellationToken = default)
    {
        // Garante que o canal esteja pronto para uso
        var channel = await GetOrCreateChannelAsync(queue, cancellationToken);

        var properties = new BasicProperties
        {
            Persistent = true
        };

        _logger.LogInformation("Publicando mensagem na fila '{QueueName}' ({Size} bytes)...", queue, body.Length);

        // Execute publish with retry policy
        await _retryPolicy.ExecuteAsync(async (ct) =>
        {
            await channel.BasicPublishAsync(
                string.Empty, // Default exchange
                queue,
                true, // Return error if message cannot be routed
                body: body,
                basicProperties: properties,
                cancellationToken: ct
            );
        }, cancellationToken);

        _logger.LogInformation("Mensagem publicada com sucesso na fila '{QueueName}'.", queue);
    }

    private async Task<IChannel> GetOrCreateChannelAsync(string queueName, CancellationToken cancellationToken)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(RabbitMqQueuePublisherImpl));

        await _channelSemaphore.WaitAsync(cancellationToken);
        try
        {
            // If channel already exists and is open, return it
            if (_channel is { IsOpen: true }) return _channel;

            _logger.LogInformation("Canal não existe ou está fechado. Criando um novo canal...");

            // Cria um novo canal
            _channel = await _persistentConnectionImpl.CreateChannelAsync(cancellationToken);
            _channel.CallbackExceptionAsync += OnChannelCallbackException;

            // Garante que a fila exista antes de publicar (idempotente)
            await _channel.QueueDeclareAsync(
                queueName,
                true,
                false,
                false,
                null,
                cancellationToken: cancellationToken);

            _logger.LogInformation("Canal criado e fila '{QueueName}' declarada com sucesso.", queueName);

            return _channel;
        }
        finally
        {
            _channelSemaphore.Release();
        }
    }

    private void OnConnectionImplReconnected()
    {
        if (_disposed) return;
        _logger.LogInformation(
            "Conexão RabbitMQ restabelecida. Limpando o canal para que seja recriado na próxima publicação.");
        _channel?.Dispose();
        _channel = null;
    }

    private Task OnChannelCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return Task.CompletedTask;
        _logger.LogWarning(e.Exception, "Exceção no callback do canal. O canal será recriado na próxima publicação.");

        _channel?.Dispose();
        _channel = null;
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        _persistentConnectionImpl.OnReconnected -= OnConnectionImplReconnected;

        try
        {
            if (_channel is not null)
            {
                _channel.CallbackExceptionAsync -= OnChannelCallbackException;
                _channel.Dispose();
            }

            _logger.LogInformation("Publisher descartado com sucesso.");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Erro ao descartar o publisher.");
        }
        finally
        {
            _channelSemaphore.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}