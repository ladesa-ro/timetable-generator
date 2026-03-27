using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Group - no more than one schedule at the same time.
/// </summary>
public static class ConstraintGroupOneScheduleAtSameTime
{
    public static void Apply(GenerationContext generationContext)
    {
        ConstraintHelpers.ApplyAtMostOnePerGroup(
            generationContext,
            p => new { p.Date, p.GroupId, p.TimeSlot });
    }
}
