using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: For the same teacher and date, no overlapping time slots may be scheduled.
/// </summary>
public class ConstraintTeacherNoOverlappingTimeSlots : IConstraint
{
    public void Apply(GenerationContext generationContext)
    {
        ConstraintHelpers.ApplyNoOverlappingTimeSlots(
            generationContext,
            p => new { p.Date, p.TeacherId });
    }
}
