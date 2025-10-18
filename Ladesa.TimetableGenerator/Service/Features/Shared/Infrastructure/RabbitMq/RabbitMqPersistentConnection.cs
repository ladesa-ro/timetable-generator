using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;

namespace Ladesa.TimetableGenerator.Service.Features.Shared.Infrastructure.RabbitMq;

public sealed class RabbitMqPersistentConnection : IAsyncDisposable
{
    private readonly RabbitMqConfig _rabbitMqConfig;
    private readonly ILogger<RabbitMqPersistentConnection> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly SemaphoreSlim _connectionSemaphore = new(1, 1);

    private IConnection? _connection;
    private bool _disposed;

    public event Action? OnReconnected;

    public RabbitMqPersistentConnection(
        RabbitMqConfig rabbitMqConfig, // Recebe a classe de configuração
        ILogger<RabbitMqPersistentConnection> logger,
        int retryCount = 5)
    {
        _rabbitMqConfig = rabbitMqConfig ?? throw new ArgumentNullException(nameof(rabbitMqConfig));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _retryPolicy = Policy.Handle<SocketException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetryAsync(
                retryCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) +
                                TimeSpan.FromMilliseconds(new Random().Next(0, 1000)),
                (ex, time, attempt, context) =>
                {
                    _logger.LogWarning(ex,
                        "RabbitMQ: Falha ao conectar. Retentando em {Time}s. Tentativa {Attempt}/{RetryCount}",
                        time.TotalSeconds, attempt, retryCount);
                }
            );
    }

    public bool IsConnected => _connection is { IsOpen: true } && !_disposed;

    public async Task<bool> TryConnectAsync(CancellationToken cancellationToken = default)
    {
        if (IsConnected) return true;

        await _connectionSemaphore.WaitAsync(cancellationToken);

        try
        {
            if (IsConnected) return true;

            _logger.LogInformation("RabbitMQ: Tentando conectar...");

            var connectionFactory = _rabbitMqConfig.GetConnectionFactory();

            connectionFactory.AutomaticRecoveryEnabled = true;
            connectionFactory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);

            var policyResult = await _retryPolicy.ExecuteAndCaptureAsync(async () =>
            {
                _connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
            });

            if (policyResult.Outcome == OutcomeType.Failure || !IsConnected || _connection is null)
            {
                _logger.LogError("RabbitMQ: Falha ao conectar após múltiplas tentativas. Erro final: {Exception}",
                    policyResult.FinalException?.Message);
                return false;
            }

            _connection.ConnectionShutdownAsync += OnConnectionShutdown;
            _connection.CallbackExceptionAsync += OnCallbackException;
            _connection.ConnectionBlockedAsync += OnConnectionBlocked;

            _logger.LogInformation("RabbitMQ: Conectado com sucesso ao host '{HostName}'",
                _connection.Endpoint.HostName);

            OnReconnected?.Invoke();

            return true;
        }
        finally
        {
            _connectionSemaphore.Release();
        }
    }

    public async Task<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default)
    {
        if (!IsConnected) await TryConnectAsync(cancellationToken);

        if (!IsConnected || _connection is null)
            throw new InvalidOperationException("Não foi possível conectar ao RabbitMQ para criar um canal.");

        return await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
    }

    #region Event Handlers

    private Task OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
    {
        if (_disposed) return Task.CompletedTask;
        _logger.LogWarning("RabbitMQ: Conexão bloqueada. Razão: {Reason}", e.Reason);
        return Task.CompletedTask;
    }

    private Task OnCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (_disposed) return Task.CompletedTask;
        _logger.LogError(e.Exception, "RabbitMQ: Uma exceção foi lançada no callback da conexão. Detalhes: {Detail}",
            e.Detail);
        return Task.CompletedTask;
    }

    private Task OnConnectionShutdown(object? sender, ShutdownEventArgs reason)
    {
        if (_disposed) return Task.CompletedTask;
        _logger.LogWarning("RabbitMQ: Conexão encerrada. Razão: {Reason}. Tentando reconectar...", reason.ReplyText);
        _ = TryConnectAsync();
        return Task.CompletedTask;
    }

    #endregion

    #region Dispose Pattern

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;
        try
        {
            if (_connection is not null)
            {
                _connection.ConnectionShutdownAsync -= OnConnectionShutdown;
                _connection.CallbackExceptionAsync -= OnCallbackException;
                _connection.ConnectionBlockedAsync -= OnConnectionBlocked;
                await _connection.CloseAsync();
                _connection.Dispose();
            }
        }
        catch (IOException ex)
        {
            _logger.LogCritical(ex, "Erro ao descartar a conexão RabbitMQ: {Message}", ex.Message);
        }
        finally
        {
            _connectionSemaphore.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    #endregion
}