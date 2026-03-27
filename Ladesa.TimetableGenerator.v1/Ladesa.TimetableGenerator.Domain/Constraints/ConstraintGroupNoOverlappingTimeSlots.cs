using Ladesa.TimetableGenerator.Domain.Generator;

namespace Ladesa.TimetableGenerator.Domain.Constraints;

/// <summary>
///     CONSTRAINT: For the same group and date, no overlapping time slots may be scheduled.
/// </summary>
public static class ConstraintGroupNoOverlappingTimeSlots
{
    public static void Apply(GenerationContext generationContext)
    {
        ConstraintHelpers.ApplyNoOverlappingTimeSlots(
            generationContext,
            p => new { p.Date, p.GroupId });
    }
}
