using Ladesa.TimetableGenerator.Application;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Application.Services;
using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using Ladesa.TimetableGenerator.Infrastructure.Solver;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

using Ladesa.TimetableGenerator.Server.Workers.Generator.Config;
using Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;
using Ladesa.TimetableGenerator.Server.Workers.Generator.Serialization;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Extensions.Startup;

public static class GeneratorWorkerStartupExtensions
{
    public static IServiceCollection AddGeneratorWorker(this IServiceCollection services)
    {
        services.AddSingleton<IGeneratorListenWorkerConfig, GeneratorListenWorkerConfigEnvironmentImpl>();
        services.AddSingleton<IAvailabilityEvaluator, IcalAvailabilityEvaluator>();
        services.AddSingleton<ICombinationGenerator, CombinationGenerator>();
        services.AddSingleton<ITimetableOptimizer, TimetableOptimizer>();
        services.AddSingleton<IGenerator, Infrastructure.Solver.Generator.Generator>();
        services.AddSingleton<ITimetableSolver, GenerateTimetablesHandler>();
        services.AddSingleton<IGenerateTimetableUseCase, GenerateTimetableUseCase>();
        services.AddSingleton<ISystemClock, SystemClock>();
        services.AddSingleton<IMessageDeserializer<ServiceGenerateRequestDto>, GenerateRequestDeserializer>();
        services.AddSingleton<IMessageSerializer<ServiceGenerateResponseDto>, GenerateResponseSerializer>();
        services.AddSingleton<IErrorMapper, ErrorMapper>();
        services.AddSingleton<GenerateResponseBuilder>();
        services.AddSingleton<GenerationRequestProcessor>();
        services.AddHostedService<GeneratorListenWorker>();

        return services;
    }
}
