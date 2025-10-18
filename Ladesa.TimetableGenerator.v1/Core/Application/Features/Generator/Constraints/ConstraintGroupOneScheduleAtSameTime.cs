using Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Core;

namespace Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Constraints;

/// <summary>
///     RESTRIÇÃO: Group - no more than one schedule at the same time.
/// </summary>
public abstract class ConstraintGroupOneScheduleAtSameTime : IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
        var allSchedulesProposalsByDateGroupIdTimeSlot =
            from scheduleProposal in generationContext.AllProposals
            group scheduleProposal by new
            {
                scheduleProposal.Date,
                scheduleProposal.GroupId,
                scheduleProposal.TimeSlot
            }
            into schedulesProposalsByDateGroupIdTimeSlot
            select new
            {
                schedulesProposalsByDateGroupIdTimeSlot.Key.Date,
                schedulesProposalsByDateGroupIdTimeSlot.Key.GroupId,
                schedulesProposalsByDateGroupIdTimeSlot.Key.TimeSlot,
                Proposals = schedulesProposalsByDateGroupIdTimeSlot.AsEnumerable()
            };

        foreach (var schedulesProposalsByDateGroupIdTimeSlot in allSchedulesProposalsByDateGroupIdTimeSlot)
        {
            if (schedulesProposalsByDateGroupIdTimeSlot == null)
                continue;

            var proposalsBoolVars = schedulesProposalsByDateGroupIdTimeSlot.Proposals
                .Select(proposal => proposal.ModelBoolVar).ToArray();

            if (proposalsBoolVars.Length == 0) continue;
            generationContext.CpModel.AddAtMostOne(proposalsBoolVars);
        }
    }
}