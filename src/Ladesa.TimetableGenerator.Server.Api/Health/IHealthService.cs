namespace Ladesa.TimetableGenerator.Server.Api.Health;

public interface IHealthService
{
    /// <summary>Returns the current health status of the service.</summary>
    object GetStatus();
}