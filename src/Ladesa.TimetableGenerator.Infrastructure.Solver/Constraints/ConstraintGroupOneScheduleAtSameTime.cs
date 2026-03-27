using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Group - no more than one schedule at the same time.
/// </summary>
public class ConstraintGroupOneScheduleAtSameTime : IConstraintGroupOneScheduleAtSameTime
{
    public void Apply(IGenerationContext context)
    {
        var generationContext = (GenerationContext)context;
        ConstraintHelpers.ApplyAtMostOnePerGroup(
            generationContext,
            p => new { p.Date, p.GroupId, p.TimeSlot });
    }
}
