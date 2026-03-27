using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: For the same teacher and date, no overlapping time slots may be scheduled.
/// </summary>
public class ConstraintTeacherNoOverlappingTimeSlots : IConstraintTeacherNoOverlappingTimeSlots
{
    public void Apply(IGenerationContext context)
    {
        var generationContext = (GenerationContext)context;
        ConstraintHelpers.ApplyNoOverlappingTimeSlots(
            generationContext,
            p => new { p.Date, p.TeacherId });
    }
}
