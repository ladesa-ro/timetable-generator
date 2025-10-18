namespace Ladesa.TimetableGenerator.Service.Features.Shared.Application.Ports;

public interface IQueueListener
{
    Task SubscribeAsync(string queue, Func<byte[], Task> handler, CancellationToken cancellationToken);
}