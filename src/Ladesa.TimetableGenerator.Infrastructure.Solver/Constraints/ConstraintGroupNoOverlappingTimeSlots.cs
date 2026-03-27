using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: For the same group and date, no overlapping time slots may be scheduled.
/// </summary>
public class ConstraintGroupNoOverlappingTimeSlots : IConstraintGroupNoOverlappingTimeSlots
{
    public void Apply(IGenerationContext context)
    {
        var generationContext = (GenerationContext)context;
        ConstraintHelpers.ApplyNoOverlappingTimeSlots(
            generationContext,
            p => new { p.Date, p.GroupId });
    }
}
