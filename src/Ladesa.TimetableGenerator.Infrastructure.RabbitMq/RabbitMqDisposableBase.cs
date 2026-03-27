namespace Ladesa.TimetableGenerator.Infrastructure.RabbitMq;

/// <summary>
///     Base class providing common dispose pattern for RabbitMQ providers.
/// </summary>
public abstract class RabbitMqDisposableBase : IAsyncDisposable
{
    private volatile bool _disposed;

    protected bool IsDisposed => _disposed;

    protected void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, GetType());
    }

    protected bool CheckDisposed() => _disposed;

    protected bool TryMarkDisposed()
    {
        if (_disposed) return false;
        _disposed = true;
        return true;
    }

    public abstract ValueTask DisposeAsync();
}
