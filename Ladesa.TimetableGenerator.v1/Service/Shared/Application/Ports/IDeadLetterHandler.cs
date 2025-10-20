namespace Ladesa.TimetableGenerator.v1.Service.Shared.Application.Ports;

public interface IDeadLetterHandler
{
    Task HandleAsync(string queue, byte[] message, Exception ex);
}