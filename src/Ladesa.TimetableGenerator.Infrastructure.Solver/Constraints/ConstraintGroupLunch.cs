using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Group - no schedules in lunchtime - at least 01:30
/// </summary>
public class ConstraintGroupLunch : IConstraintGroupLunch
{
    public void Apply(IGenerationContext context)
    {
        var generationContext = (GenerationContext)context;
        var lunchBefore = TimeSlotConstants.LunchBufferBefore;
        var lunchAfter = TimeSlotConstants.LunchBufferAfter;

        ConstraintHelpers.ApplyAtMostOnePerGroup(
            generationContext,
            p => new { p.Date, p.GroupId },
            p => lunchBefore.Contains(p.TimeSlot.End) || lunchAfter.Contains(p.TimeSlot.Start));
    }
}
