using Ladesa.TimetableGenerator.v1.Service.Features.Health.Ports;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Ladesa.TimetableGenerator.v1.Service.Features.Health;

public static class HealthEndpoints
{
    public static WebApplication MapHealthEndpoints(this WebApplication app)
    {
        app.MapGet("/api/health", (IHealthService healthService) =>
        {
            var status = healthService.GetStatus();
            return Results.Ok(status);
        });

        return app;
    }
}