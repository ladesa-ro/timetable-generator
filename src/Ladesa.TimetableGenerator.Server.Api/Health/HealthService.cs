namespace Ladesa.TimetableGenerator.Server.Api.Health;

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