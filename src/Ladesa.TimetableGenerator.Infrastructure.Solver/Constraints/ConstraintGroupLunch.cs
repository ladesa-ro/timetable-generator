using Ladesa.TimetableGenerator.Infrastructure.Solver.Constants;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Group - no schedules in lunchtime - at least 01:30
/// </summary>
public class ConstraintGroupLunch : IConstraint
{
    public void Apply(GenerationContext generationContext)
    {
        var lunchBefore = TimeSlotConstants.LunchBufferBefore;
        var lunchAfter = TimeSlotConstants.LunchBufferAfter;

        ConstraintHelpers.ApplyAtMostOnePerGroup(
            generationContext,
            p => new { p.Date, p.GroupId },
            p => lunchBefore.Contains(p.TimeSlot.End) || lunchAfter.Contains(p.TimeSlot.Start));
    }
}
