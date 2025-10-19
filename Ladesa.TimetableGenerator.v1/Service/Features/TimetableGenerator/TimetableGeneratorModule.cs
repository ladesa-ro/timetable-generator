using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.Workers;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator;

public static class TimetableGeneratorModule
{
    public static IServiceCollection AddModuleTimetableGenerator(this IServiceCollection services)
    {
        services.AddHostedService<TimetableGeneratorListenWorker>();

        return services;
    }
}