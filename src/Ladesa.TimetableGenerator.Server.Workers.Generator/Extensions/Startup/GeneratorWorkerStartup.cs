using Ladesa.TimetableGenerator.Application.Generator;
using Ladesa.TimetableGenerator.Application.Generator.DTOs;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Infrastructure.Solver;

using Ladesa.TimetableGenerator.Server.Workers.Generator.Config;
using Ladesa.TimetableGenerator.Server.Workers.Generator.Serialization;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Extensions.Startup;

public static class GeneratorWorkerStartupExtensions
{
    public static IServiceCollection AddGeneratorWorker(this IServiceCollection services)
    {
        services.AddSingleton<IGeneratorListenWorkerConfig, GeneratorListerWorkerConfigEnvironmentImpl>();
        services.AddSingleton<IAvailabilityEvaluator, IcalAvailabilityEvaluator>();
        services.AddSingleton<IScheduleCombinationGenerator, ScheduleCombinationGenerator>();
        services.AddSingleton<IGenerator, Infrastructure.Solver.Generator.Generator>();
        services.AddSingleton<ITimetableGeneratorService, TimetableGeneratorService>();
        services.AddSingleton<ISystemClock, SystemClock>();
        services.AddSingleton<IMessageDeserializer<ServiceGenerateRequestDto>, GenerateRequestDeserializer>();
        services.AddSingleton<IMessageSerializer<ServiceGenerateResponseDto>, GenerateResponseSerializer>();
        services.AddSingleton<IErrorMapper, ErrorMapper>();
        services.AddSingleton<GenerateResponseBuilder>();
        services.AddHostedService<GeneratorListenWorker>();

        return services;
    }
}
