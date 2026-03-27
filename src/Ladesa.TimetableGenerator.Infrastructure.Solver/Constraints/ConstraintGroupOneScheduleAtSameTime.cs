using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Group - no more than one schedule at the same time.
/// </summary>
public class ConstraintGroupOneScheduleAtSameTime : IConstraint
{
    public void Apply(GenerationContext generationContext)
    {
        ConstraintHelpers.ApplyAtMostOnePerGroup(
            generationContext,
            p => new { p.Date, p.GroupId, p.TimeSlot });
    }
}
