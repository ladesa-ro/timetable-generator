using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no more than one schedule at the same time.
/// </summary>
public class ConstraintTeacherOneScheduleAtSameTime : IConstraint
{
    public void Apply(GenerationContext generationContext)
    {
        ConstraintHelpers.ApplyAtMostOnePerGroup(
            generationContext,
            p => new { p.Date, p.TeacherId, p.TimeSlot });
    }
}
