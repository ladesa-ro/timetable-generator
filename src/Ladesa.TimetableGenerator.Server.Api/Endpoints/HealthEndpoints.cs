using Ladesa.TimetableGenerator.Server.Api.Health;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Ladesa.TimetableGenerator.Server.Api.Endpoints;

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