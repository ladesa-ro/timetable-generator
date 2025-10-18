using Ladesa.TimetableGenerator.Core.Application.Features.Generator.Core;
using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Application.Features.Generator.Constraints;

/// <summary>
///     CONSTRAINT: Group - no schedules in lunchtime - at least 01:30
/// </summary>
public class ConstraintGroupLunch: IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
        var before12 = new TimeSlot("11:30:00", "12:00:00");
        var after12 = new TimeSlot("13:00:00", "13:30:00");

        var allSchedulesProposalsByDateGroupId =
            from scheduleProposal in generationContext.AllProposals
            where
                before12.Verify(
                    scheduleProposal.TimeSlot.End
                )
                ||
                after12.Verify(
                    scheduleProposal.TimeSlot.Start
                )
            group scheduleProposal by new
            {
                scheduleProposal.Date, 
                scheduleProposal.GroupId
            }
            into schedulesProposalsByDateGroupId
            select new
            {
                schedulesProposalsByDateGroupId.Key.Date,
                schedulesProposalsByDateGroupId.Key.GroupId,
                Proposals = schedulesProposalsByDateGroupId.AsEnumerable()
            };

        foreach (var schedulesProposalsByDateGroupId in allSchedulesProposalsByDateGroupId)
        {
            if (schedulesProposalsByDateGroupId == null)
                continue;

            var proposals = schedulesProposalsByDateGroupId.Proposals.Select(proposal => proposal.ModelBoolVar).ToList();

            if (proposals.Count != 0)
                generationContext.CpModel.AddAtMostOne(proposals);
        }
    }
}