namespace Ladesa.TimetableGenerator.Server.Api.Health;

public class HealthService : IHealthService
{
    public HealthStatus GetStatus()
    {
        return new HealthStatus("up", "timetable-generator", DateTimeOffset.UtcNow);
    }
}
