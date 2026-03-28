using Ladesa.TimetableGenerator.Application.Ports;

namespace Ladesa.TimetableGenerator.Domain.Test.TestDoubles;

public class InMemoryDeadLetterHandler : IDeadLetterHandler
{
    public List<(string Queue, byte[] Message, Exception Error)> DeadLetters { get; } = [];

    public Task HandleAsync(string queue, byte[] message, Exception ex)
    {
        DeadLetters.Add((queue, message, ex));
        return Task.CompletedTask;
    }
}
