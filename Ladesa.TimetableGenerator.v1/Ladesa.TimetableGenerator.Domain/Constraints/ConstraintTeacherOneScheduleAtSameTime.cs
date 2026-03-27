using Ladesa.TimetableGenerator.Domain.Generator;

namespace Ladesa.TimetableGenerator.Domain.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no more than one schedule at the same time.
/// </summary>
public static class ConstraintTeacherOneScheduleAtSameTime
{
    public static void Apply(GenerationContext generationContext)
    {
        ConstraintHelpers.ApplyAtMostOnePerGroup(
            generationContext,
            p => new { p.Date, p.TeacherId, p.TimeSlot });
    }
}
