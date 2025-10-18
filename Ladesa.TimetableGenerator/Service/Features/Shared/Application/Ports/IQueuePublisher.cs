namespace Ladesa.TimetableGenerator.Service.Features.Shared.Application.Ports;

public interface IQueuePublisher
{
    Task PublishAsync(string queue, byte[] bytes, CancellationToken cancellationToken);
}