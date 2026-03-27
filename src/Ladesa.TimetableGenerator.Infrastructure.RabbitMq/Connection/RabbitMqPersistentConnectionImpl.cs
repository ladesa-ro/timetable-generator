using System.Net.Sockets;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;

public sealed class RabbitMqPersistentConnectionImpl : RabbitMqDisposableBase, IRabbitMqPersistentConnection
{
    private const double RetryBackoffBase = 2.0;
    private const int MaxJitterMilliseconds = 1000;
    private static readonly TimeSpan NetworkRecoveryInterval = TimeSpan.FromSeconds(10);

    private readonly IRabbitMqConnectionFactory _rabbitMqConnectionFactoryImpl;
    private readonly ILogger<RabbitMqPersistentConnectionImpl> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly SemaphoreSlim _connectionSemaphore = new(1, 1);

    private IConnection? _connection;

    public event Action? OnReconnected;

    public RabbitMqPersistentConnectionImpl(
        IRabbitMqConnectionFactory rabbitMqConnectionFactoryImpl,
        ILogger<RabbitMqPersistentConnectionImpl> logger,
        int retryCount = 5)
    {
        _rabbitMqConnectionFactoryImpl = rabbitMqConnectionFactoryImpl ?? throw new ArgumentNullException(nameof(rabbitMqConnectionFactoryImpl));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _retryPolicy = Policy.Handle<SocketException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetryAsync(
                retryCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(RetryBackoffBase, retryAttempt)) +
                                TimeSpan.FromMilliseconds(Random.Shared.Next(0, MaxJitterMilliseconds)),
                (ex, time, attempt, context) =>
                {
                    _logger.LogWarning(ex,
                        "RabbitMQ: Failed to connect. Retrying in {Time}s. Attempt {Attempt}/{RetryCount}",
                        time.TotalSeconds, attempt, retryCount);
                }
            );
    }

    public bool IsConnected => _connection is { IsOpen: true } && !IsDisposed;

    public async Task<bool> TryConnectAsync(CancellationToken cancellationToken = default)
    {
        if (IsConnected) return true;

        await _connectionSemaphore.WaitAsync(cancellationToken);

        try
        {
            if (IsConnected) return true;

            _logger.LogInformation("RabbitMQ: Attempting to connect...");

            if (!await EstablishConnectionAsync(cancellationToken))
                return false;

            RegisterConnectionEventHandlers();

            _logger.LogInformation("RabbitMQ: Connected successfully to host '{HostName}'",
                _connection!.Endpoint.HostName);

            OnReconnected?.Invoke();

            return true;
        }
        finally
        {
            _connectionSemaphore.Release();
        }
    }

    private async Task<bool> EstablishConnectionAsync(CancellationToken cancellationToken)
    {
        var connectionFactory = _rabbitMqConnectionFactoryImpl.GetConnectionFactory();
        connectionFactory.AutomaticRecoveryEnabled = true;
        connectionFactory.NetworkRecoveryInterval = NetworkRecoveryInterval;

        var policyResult = await _retryPolicy.ExecuteAndCaptureAsync(async () =>
        {
            _connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        });

        if (policyResult.Outcome == OutcomeType.Failure || !IsConnected || _connection is null)
        {
            _logger.LogError("RabbitMQ: Failed to connect after multiple attempts. Final error: {Exception}",
                policyResult.FinalException?.Message);
            return false;
        }

        return true;
    }

    private void RegisterConnectionEventHandlers()
    {
        _connection!.ConnectionShutdownAsync += OnConnectionShutdown;
        _connection.CallbackExceptionAsync += OnCallbackException;
        _connection.ConnectionBlockedAsync += OnConnectionBlocked;
    }

    public async Task<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default)
    {
        if (!IsConnected) await TryConnectAsync(cancellationToken);

        if (!IsConnected || _connection is null)
            throw new InvalidOperationException("Could not connect to RabbitMQ to create a channel.");

        return await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
    }

    private Task OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
    {
        if (CheckDisposed()) return Task.CompletedTask;
        _logger.LogWarning("RabbitMQ: Connection blocked. Reason: {Reason}", e.Reason);
        return Task.CompletedTask;
    }

    private Task OnCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (CheckDisposed()) return Task.CompletedTask;
        _logger.LogError(e.Exception, "RabbitMQ: Exception in connection callback. Details: {Detail}", e.Detail);
        return Task.CompletedTask;
    }

    private Task OnConnectionShutdown(object? sender, ShutdownEventArgs reason)
    {
        if (CheckDisposed()) return Task.CompletedTask;
        _logger.LogWarning("RabbitMQ: Connection shut down. Reason: {Reason}. Attempting to reconnect...", reason.ReplyText);
        _ = TryConnectAsync();
        return Task.CompletedTask;
    }

    public override async ValueTask DisposeAsync()
    {
        if (!TryMarkDisposed()) return;

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
            _logger.LogCritical(ex, "Error disposing RabbitMQ connection: {Message}", ex.Message);
        }
        finally
        {
            _connectionSemaphore.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}
