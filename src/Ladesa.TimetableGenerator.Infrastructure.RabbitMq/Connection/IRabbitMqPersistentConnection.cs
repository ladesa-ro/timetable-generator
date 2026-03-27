using RabbitMQ.Client;

namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq.Connection;

public interface IRabbitMqPersistentConnection : IAsyncDisposable
{
    bool IsConnected { get; }
    event Action? OnReconnected;
    Task<bool> TryConnectAsync(CancellationToken cancellationToken = default);
    Task<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default);
}
