using Ladesa.TimetableGenerator.Application.Ports;

namespace Ladesa.TimetableGenerator.Domain.Test.TestDoubles;

public class InMemoryQueueListener : IQueueListener
{
    private readonly List<(string Queue, Func<byte[], Task> Handler)> _subscriptions = [];

    public IReadOnlyList<(string Queue, Func<byte[], Task> Handler)> Subscriptions => _subscriptions;

    public Task SubscribeAsync(string queue, Func<byte[], Task> handler, CancellationToken cancellationToken)
    {
        _subscriptions.Add((queue, handler));
        return Task.CompletedTask;
    }

    /// <summary>Simulates receiving a message on a given queue.</summary>
    public async Task SimulateMessageAsync(string queue, byte[] body)
    {
        foreach (var (q, handler) in _subscriptions.Where(s => s.Queue == queue))
            await handler(body);
    }
}
