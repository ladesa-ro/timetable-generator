using Ladesa.TimetableGenerator.Domain.Constants;
using Ladesa.TimetableGenerator.Domain.Generator;

namespace Ladesa.TimetableGenerator.Domain.Constraints;

/// <summary>
///     CONSTRAINT: Group - no schedules in lunchtime - at least 01:30
/// </summary>
public static class ConstraintGroupLunch
{
    public static void Apply(GenerationContext generationContext)
    {
        var lunchBefore = TimeSlotConstants.LunchBufferBefore;
        var lunchAfter = TimeSlotConstants.LunchBufferAfter;

        ConstraintHelpers.ApplyAtMostOnePerGroup(
            generationContext,
            p => new { p.Date, p.GroupId },
            p => lunchBefore.Contains(p.TimeSlot.End) || lunchAfter.Contains(p.TimeSlot.Start));
    }
}
