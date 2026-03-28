using Ladesa.TimetableGenerator.Application.Ports;

namespace Ladesa.TimetableGenerator.Domain.Test.TestDoubles;

public class InMemoryQueuePublisher : IQueuePublisher
{
    public List<(string Queue, byte[] Body)> Messages { get; } = [];

    public Task PublishAsync(string queue, byte[] bytes, CancellationToken cancellationToken)
    {
        Messages.Add((queue, bytes));
        return Task.CompletedTask;
    }
}
