namespace Ladesa.TimetableGenerator.Server.Api.Health;

public record HealthStatus(string Status, string Service, DateTimeOffset Timestamp);

public interface IHealthService
{
    HealthStatus GetStatus();
}
