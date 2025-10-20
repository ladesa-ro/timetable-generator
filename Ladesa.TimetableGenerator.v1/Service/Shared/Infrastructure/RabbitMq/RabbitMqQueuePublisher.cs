using System.Net.Sockets;
using Ladesa.TimetableGenerator.v1.Service.Shared.Application.Ports;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.RabbitMq;

/// <summary>
/// Publicador de mensagens RabbitMQ resiliente que gerencia e reutiliza seu próprio canal,
/// garante a existência da fila e publica mensagens persistentes com política de retry.
/// </summary>
public sealed class RabbitMqQueuePublisher : IQueuePublisher, IAsyncDisposable
{
    private readonly RabbitMqPersistentConnection _persistentConnection;
    private readonly ILogger<RabbitMqQueuePublisher> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly SemaphoreSlim _channelSemaphore = new(1, 1);

    private IChannel? _channel;
    private bool _disposed;

    public RabbitMqQueuePublisher(
        RabbitMqPersistentConnection persistentConnection,
        ILogger<RabbitMqQueuePublisher> logger,
        int retryCount = 3)
    {
        _persistentConnection = persistentConnection;
        _logger = logger;

        // Assina o evento para saber quando recriar o canal
        _persistentConnection.OnReconnected += OnConnectionReconnected;

        // Política de retry para a operação de publicação
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
    /// Publica uma mensagem numa fila específica de forma assíncrona e resiliente.
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

        // Executa a publicação com a política de retry
        await _retryPolicy.ExecuteAsync(async (ct) =>
        {
            await channel.BasicPublishAsync(
                string.Empty, // Default exchange
                queue,
                true, // Retorna erro se a mensagem não puder ser roteada
                body: body,
                basicProperties: properties,
                cancellationToken: ct
            );
        }, cancellationToken);

        _logger.LogInformation("Mensagem publicada com sucesso na fila '{QueueName}'.", queue);
    }

    private async Task<IChannel> GetOrCreateChannelAsync(string queueName, CancellationToken cancellationToken)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(RabbitMqQueuePublisher));

        await _channelSemaphore.WaitAsync(cancellationToken);
        try
        {
            // Se o canal já existe e está aberto, retorna-o
            if (_channel is { IsOpen: true }) return _channel;

            _logger.LogInformation("Canal não existe ou está fechado. Criando um novo canal...");

            // Cria um novo canal
            _channel = await _persistentConnection.CreateChannelAsync(cancellationToken);
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

    private void OnConnectionReconnected()
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

        _persistentConnection.OnReconnected -= OnConnectionReconnected;

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