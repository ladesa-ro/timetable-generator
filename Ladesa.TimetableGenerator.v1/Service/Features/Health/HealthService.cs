using Ladesa.TimetableGenerator.v1.Service.Features.Health.Ports;

namespace Ladesa.TimetableGenerator.v1.Service.Features.Health;

public class HealthService : IHealthService
{
    public object GetStatus()
    {
        return new
        {
            status = "up",
            service = "timetable-generator",
            timestamp = DateTimeOffset.UtcNow
        };
    }
}