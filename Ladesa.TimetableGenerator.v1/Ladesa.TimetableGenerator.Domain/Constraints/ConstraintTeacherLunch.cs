using Ladesa.TimetableGenerator.Domain.Constants;
using Ladesa.TimetableGenerator.Domain.Generator;

namespace Ladesa.TimetableGenerator.Domain.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no schedules in lunchtime - at least 01:30
/// </summary>
public static class ConstraintTeacherLunch
{
    public static void Apply(GenerationContext generationContext)
    {
        var lunchBefore = TimeSlotConstants.LunchBufferBefore;
        var lunchAfter = TimeSlotConstants.LunchBufferAfter;

        ConstraintHelpers.ApplyAtMostOnePerGroup(
            generationContext,
            p => new { p.Date, p.TeacherId },
            p => lunchBefore.Contains(p.TimeSlot.End) || lunchAfter.Contains(p.TimeSlot.Start));
    }
}
