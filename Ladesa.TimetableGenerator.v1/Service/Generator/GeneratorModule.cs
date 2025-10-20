
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.Workers;

namespace Ladesa.TimetableGenerator.v1.Service.Generator;

public static class GeneratorModule
{
    public static IServiceCollection AddModuleTimetableGenerator(this IServiceCollection services)
    {
        services.AddHostedService<TimetableGeneratorListenWorker>();

        return services;
    }
}