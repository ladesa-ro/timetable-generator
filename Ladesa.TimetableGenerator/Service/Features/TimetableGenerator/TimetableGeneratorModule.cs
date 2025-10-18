using Ladesa.TimetableGenerator.Service.Features.TimetableGenerator.Application.Workers;

namespace Ladesa.TimetableGenerator.Service.Features.TimetableGenerator;

public static class TimetableGeneratorModule
{
    public static IServiceCollection AddModuleTimetableGenerator(this IServiceCollection services)
    {
        services.AddHostedService<TimetableGeneratorListenWorker>();

        return services;
    }
}