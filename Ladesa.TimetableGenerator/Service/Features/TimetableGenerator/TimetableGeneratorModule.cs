using Ladesa.TimetableGenerator.Service.Workers;

namespace Ladesa.TimetableGenerator.Service.Features.Health;

public static class TimetableGeneratorModule
{
    public static IServiceCollection AddModuleTimetableGenerator(this IServiceCollection services)
    {
        services.AddHostedService<TimetableGeneratorListenWorker>();

        return services;
    }
}