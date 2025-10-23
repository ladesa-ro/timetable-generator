using Ladesa.TimetableGenerator.v1.Core.Generator;
using Ladesa.TimetableGenerator.v1.Core.Domain;

namespace Ladesa.TimetableGenerator.v1.Core.Constraints;

/// <summary>
/// Restriction: For the same group and date, no overlapping time slots may be scheduled.
/// </summary>
public abstract class ConstraintGroupNoOverlappingTimeSlots : IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
        var proposalsByDateAndGroup = 
            from scheduleProposal in generationContext.AllProposals
            group scheduleProposal by new
            {
                scheduleProposal.Date, 
                scheduleProposal.GroupId
            } into g
            select new { g.Key.Date, g.Key.GroupId, Proposals = g.AsEnumerable().ToArray() };

        foreach (var bucket in proposalsByDateAndGroup)
        {
            var proposals = bucket.Proposals;
            for (var i = 0; i < proposals.Length; i++)
            {
                for (var j = i + 1; j < proposals.Length; j++)
                {
                    if (!Overlap(proposals[i].TimeSlot, proposals[j].TimeSlot, bucket.Date)) continue;
                    
                    var a = proposals[i].ModelBoolVar;
                    var b = proposals[j].ModelBoolVar;
                    generationContext.CpModel.AddAtMostOne([a, b]);
                }
            }
        }
    }

    private static bool Overlap(TimeSlot a, TimeSlot b, DateOnly date)
    {
        var (aStart, aEnd) = a.GetDateTimeRange(date);
        var (bStart, bEnd) = b.GetDateTimeRange(date);
        return aStart < bEnd && aEnd > bStart;
    }
}
