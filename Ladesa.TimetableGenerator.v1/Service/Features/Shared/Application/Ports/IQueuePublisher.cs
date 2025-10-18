namespace Ladesa.TimetableGenerator.v1.Service.Features.Shared.Application.Ports;

public interface IQueuePublisher
{
    Task PublishAsync(string queue, byte[] bytes, CancellationToken cancellationToken);
}