namespace Ladesa.TimetableGenerator.v1.Service.Shared.Application.Ports;

public interface IQueueListener
{
    Task SubscribeAsync(string queue, Func<byte[], Task> handler, CancellationToken cancellationToken);
}