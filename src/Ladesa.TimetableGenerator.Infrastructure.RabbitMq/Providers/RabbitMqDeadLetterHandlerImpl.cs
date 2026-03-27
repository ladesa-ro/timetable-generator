using System.Text;
using System.Text.Json;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;
using Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Constants;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Providers;

public class RabbitMqDeadLetterHandlerImpl : RabbitMqDisposableBase, IDeadLetterHandler
{
    private readonly IRabbitMqPersistentConnection _persistentConnection;
    private readonly ILogger<RabbitMqDeadLetterHandlerImpl> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;

    private IChannel? _channel;

    public RabbitMqDeadLetterHandlerImpl(
        IRabbitMqPersistentConnection persistentConnection,
        ILogger<RabbitMqDeadLetterHandlerImpl> logger,
        int retryCount = 3)
    {
        _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount,
                attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)) +
                           TimeSpan.FromMilliseconds(new Random().Next(0, 200)),
                (exception, timeSpan, retryAttempt, context) =>
                {
                    _logger.LogWarning(exception,
                        "Failed to publish to DLQ. Attempt {RetryAttempt}/{RetryCount}. Next retry in {Delay}s.",
                        retryAttempt, retryCount, timeSpan.TotalSeconds);
                });
    }

    private async Task<IChannel> GetOrCreateChannelAsync(CancellationToken cancellationToken = default)
    {
        if (_channel != null && _channel.IsOpen)
            return _channel;

        if (!await _persistentConnection.TryConnectAsync(cancellationToken))
            throw new InvalidOperationException("Could not connect to RabbitMQ to create DLQ channel.");

        _channel = await _persistentConnection.CreateChannelAsync(cancellationToken);
        return _channel;
    }

    public async Task HandleAsync(string queue, byte[] message, Exception exception)
    {
        ThrowIfDisposed();
        if (string.IsNullOrWhiteSpace(queue)) throw new ArgumentNullException(nameof(queue));

        var dlqName = RabbitMqNamingConventions.GetDlqName(queue);
        var dlxName = RabbitMqNamingConventions.GetDlxName(queue);

        try
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var channel = await GetOrCreateChannelAsync();

                await channel.ExchangeDeclareAsync(dlxName, ExchangeType.Fanout, true, false);
                await channel.QueueDeclareAsync(dlqName, true, false, false);
                await channel.QueueBindAsync(dlqName, dlxName, "");

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
                _logger.LogInformation("Message sent to DLQ '{DLQName}' successfully.", dlqName);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Critical failure sending message to DLQ '{DLQName}'.", dlqName);
            throw;
        }
    }

    public override async ValueTask DisposeAsync()
    {
        if (!TryMarkDisposed()) return;

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
            _logger.LogWarning(ex, "Error disposing DLQ channel.");
        }

        GC.SuppressFinalize(this);
    }
}
