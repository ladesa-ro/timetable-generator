using Ladesa.TimetableGenerator.Service.Features.Health.Presentation.Endpoints;
using Ladesa.TimetableGenerator.Service.Features.Health.Services;
using Microsoft.AspNetCore.Builder;

namespace Ladesa.TimetableGenerator.Service.Features.Health;

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
