using Ladesa.TimetableGenerator.Application.Health;

namespace Ladesa.TimetableGenerator.Server.Api.Extensions.Startup;

public static class TimetableGeneratorApplicationExtensions
{
    public static IServiceCollection AddTimetableGeneratorApplication(this IServiceCollection services)
    {
        services.AddSingleton<IHealthService, HealthService>();

        return services;
    }
}
