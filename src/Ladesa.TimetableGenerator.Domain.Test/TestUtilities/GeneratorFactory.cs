using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Domain.Test.TestUtilities;

/// <summary>
///     Factory for creating a fully-wired Generator instance with all default constraints
///     for use in integration tests.
/// </summary>
public static class GeneratorFactory
{
    public static Generator CreateDefault()
    {
        var constraints = new IConstraint[]
        {
            new ConstraintGroupOneScheduleAtSameTime(),
            new ConstraintTeacherOneScheduleAtSameTime(),
            new ConstraintDiaryLimitSchedulesInOneWeek(),
            new ConstraintDiaryLimitRemaining(),
            new ConstraintTeacherLunch(),
            new ConstraintGroupLunch(),
            new ConstraintTeacherNoOppositeTurns(),
            new ConstraintTeacher12Hours(),
            new ConstraintGroupNoOverlappingTimeSlots(),
            new ConstraintTeacherNoOverlappingTimeSlots(),
        };

        var optimizer = new TimetableOptimizer();
        var combinationGenerator = new ScheduleCombinationGenerator();

        return new Generator(constraints, optimizer, combinationGenerator);
    }
}
