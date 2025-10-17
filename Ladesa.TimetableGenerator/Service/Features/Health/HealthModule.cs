using Ladesa.TimetableGenerator.Service.Features.Health.Services;

namespace Ladesa.TimetableGenerator.Service.Features.Health;

public static class HealthModule
{
    public static IServiceCollection AddHealthModule(this IServiceCollection services)
    {
        services.AddScoped<IHealthService, HealthService>();
        return services;
    }
}