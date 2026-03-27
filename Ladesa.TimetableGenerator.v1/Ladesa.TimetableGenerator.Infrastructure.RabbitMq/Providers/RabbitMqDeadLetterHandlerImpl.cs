using System.Text;
using System.Text.Json;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Providers;

public class RabbitMqDeadLetterHandlerImpl : IDeadLetterHandler, IAsyncDisposable
{
    private readonly RabbitMqPersistentConnectionImpl _persistentConnectionImpl;
    private readonly ILogger<RabbitMqDeadLetterHandlerImpl> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;

    private IChannel? _channel;
    private bool _disposed;

    public RabbitMqDeadLetterHandlerImpl(
        RabbitMqPersistentConnectionImpl persistentConnectionImpl,
        ILogger<RabbitMqDeadLetterHandlerImpl> logger,
        int retryCount = 3)
    {
        _persistentConnectionImpl = persistentConnectionImpl ?? throw new ArgumentNullException(nameof(persistentConnectionImpl));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Exponential retry policy
        _retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount,
                attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)) +
                           TimeSpan.FromMilliseconds(new Random().Next(0, 200)),
                (exception, timeSpan, retryAttempt, context) =>
                {
                    _logger.LogWarning(exception,
                        "Falha ao publicar na DLQ. Tentativa {RetryAttempt}/{RetryCount}. Próxima tentativa em {Delay}s.",
                        retryAttempt, retryCount, timeSpan.TotalSeconds);
                });
    }

    private async Task<IChannel> GetOrCreateChannelAsync(CancellationToken cancellationToken = default)
    {
        if (_channel != null && _channel.IsOpen)
            return _channel;

        if (!await _persistentConnectionImpl.TryConnectAsync(cancellationToken))
            throw new InvalidOperationException("Não foi possível conectar ao RabbitMQ para criar o canal DLQ.");

        _channel = await _persistentConnectionImpl.CreateChannelAsync(cancellationToken);
        return _channel;
    }

    public async Task HandleAsync(string queue, byte[] message, Exception exception)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(RabbitMqDeadLetterHandlerImpl));
        if (string.IsNullOrWhiteSpace(queue)) throw new ArgumentNullException(nameof(queue));

        var dlqName = $"dlq.{queue}";
        var dlxName = $"dlx.{queue}";

        try
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var channel = await GetOrCreateChannelAsync();

                // Declara exchange e fila DLQ de forma idempotente
                await channel.ExchangeDeclareAsync(dlxName, ExchangeType.Fanout, true, false);
                await channel.QueueDeclareAsync(dlqName, true, false, false);
                await channel.QueueBindAsync(dlqName, dlxName, "");

                // Serialize message with error information
                var payload = JsonSerializer.Serialize(new
                {
                    Message = message,
                    Error = exception?.Message,
                    ExceptionType = exception?.GetType().FullName,
                    StackTrace = exception?.StackTrace,
                    Timestamp = DateTime.UtcNow
                });

                var body = Encoding.UTF8.GetBytes(payload);

                await channel.BasicPublishAsync(dlxName, "", body);
                _logger.LogInformation("Mensagem enviada para DLQ '{DLQName}' com sucesso.", dlqName);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha crítica ao enviar mensagem para DLQ '{DLQName}'.", dlqName);
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;

        try
        {
            if (_channel != null)
            {
                if (_channel.IsOpen)
                    await _channel.CloseAsync();

                _channel.Dispose();
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Erro ao descartar o canal DLQ.");
        }

        GC.SuppressFinalize(this);
    }
}