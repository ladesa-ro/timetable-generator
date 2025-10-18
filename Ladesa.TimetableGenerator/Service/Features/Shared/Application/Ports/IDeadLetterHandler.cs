namespace Ladesa.TimetableGenerator.Service.Features.Shared.Application.Ports;

public interface IDeadLetterHandler
{
    Task HandleAsync(string queue, byte[] message, Exception ex);
}