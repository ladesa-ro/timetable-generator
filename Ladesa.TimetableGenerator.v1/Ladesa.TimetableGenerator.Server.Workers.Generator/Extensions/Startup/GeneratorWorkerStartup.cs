using Ladesa.TimetableGenerator.Application.Generator;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Server.Workers.Generator.Config;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Extensions.Startup;

public static class GeneratorWorkerStartupExtensions
{
    public static IServiceCollection AddGeneratorWorker(this IServiceCollection services)
    {
        services.AddSingleton<IGeneratorListenWorkerConfig, GeneratorListerWorkerConfigEnvironmentImpl>();
        services.AddSingleton<ITimetableGeneratorService, TimetableGeneratorService>();
        services.AddHostedService<GeneratorListenWorker>();

        return services;
    }
}
