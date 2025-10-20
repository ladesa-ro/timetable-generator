using Ladesa.TimetableGenerator.v1.Service.Health.Application;
using Ladesa.TimetableGenerator.v1.Service.Health.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;

namespace Ladesa.TimetableGenerator.v1.Service.Health;

public static class HealthModule
{
    public static IServiceCollection AddHealthModule(this IServiceCollection services)
    {
        services.AddScoped<IHealthService, HealthService>();
        return services;
    }

    public static WebApplication UseAppHealth(this WebApplication app)
    {
        app.MapHealthEndpoints();
        return app;
    }
}