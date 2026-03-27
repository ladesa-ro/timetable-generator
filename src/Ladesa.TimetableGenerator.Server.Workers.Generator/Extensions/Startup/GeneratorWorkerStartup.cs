using Ladesa.TimetableGenerator.Application.Generator;
using Ladesa.TimetableGenerator.Application.Generator.DTOs;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Infrastructure.Solver;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;
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
        services.AddSingleton<ITimetableOptimizer, TimetableOptimizer>();

        services.AddSingleton<IConstraintGroupOneScheduleAtSameTime, ConstraintGroupOneScheduleAtSameTime>();
        services.AddSingleton<IConstraintTeacherOneScheduleAtSameTime, ConstraintTeacherOneScheduleAtSameTime>();
        services.AddSingleton<IConstraintDiaryLimitSchedulesInOneWeek, ConstraintDiaryLimitSchedulesInOneWeek>();
        services.AddSingleton<IConstraintDiaryLimitRemaining, ConstraintDiaryLimitRemaining>();
        services.AddSingleton<IConstraintTeacherLunch, ConstraintTeacherLunch>();
        services.AddSingleton<IConstraintGroupLunch, ConstraintGroupLunch>();
        services.AddSingleton<IConstraintTeacherNoOppositeTurns, ConstraintTeacherNoOppositeTurns>();
        services.AddSingleton<IConstraintTeacher12Hours, ConstraintTeacher12Hours>();
        services.AddSingleton<IConstraintGroupNoOverlappingTimeSlots, ConstraintGroupNoOverlappingTimeSlots>();
        services.AddSingleton<IConstraintTeacherNoOverlappingTimeSlots, ConstraintTeacherNoOverlappingTimeSlots>();

        services.AddSingleton<IEnumerable<IConstraint>>(sp => new IConstraint[]
        {
            sp.GetRequiredService<IConstraintGroupOneScheduleAtSameTime>(),
            sp.GetRequiredService<IConstraintTeacherOneScheduleAtSameTime>(),
            sp.GetRequiredService<IConstraintDiaryLimitSchedulesInOneWeek>(),
            sp.GetRequiredService<IConstraintDiaryLimitRemaining>(),
            sp.GetRequiredService<IConstraintTeacherLunch>(),
            sp.GetRequiredService<IConstraintGroupLunch>(),
            sp.GetRequiredService<IConstraintTeacherNoOppositeTurns>(),
            sp.GetRequiredService<IConstraintTeacher12Hours>(),
            sp.GetRequiredService<IConstraintGroupNoOverlappingTimeSlots>(),
            sp.GetRequiredService<IConstraintTeacherNoOverlappingTimeSlots>(),
        });

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
