using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: For the same teacher and date, no overlapping time slots may be scheduled.
/// </summary>
public static class ConstraintTeacherNoOverlappingTimeSlots
{
    public static void Apply(GenerationContext generationContext)
    {
        ConstraintHelpers.ApplyNoOverlappingTimeSlots(
            generationContext,
            p => new { p.Date, p.TeacherId });
    }
}
