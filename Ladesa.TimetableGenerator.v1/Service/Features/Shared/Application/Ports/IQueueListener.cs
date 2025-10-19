namespace Ladesa.TimetableGenerator.v1.Service.Features.Shared.Application.Ports;

public interface IQueueListener
{
    Task SubscribeAsync(string queue, Func<byte[], Task> handler, CancellationToken cancellationToken);
}