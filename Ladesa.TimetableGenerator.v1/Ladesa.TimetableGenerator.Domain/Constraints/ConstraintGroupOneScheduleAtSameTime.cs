using Ladesa.TimetableGenerator.Domain.Generator;

namespace Ladesa.TimetableGenerator.Domain.Constraints;

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
