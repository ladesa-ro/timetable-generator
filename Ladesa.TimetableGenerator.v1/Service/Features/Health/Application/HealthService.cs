namespace Ladesa.TimetableGenerator.v1.Service.Features.Health.Application;

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