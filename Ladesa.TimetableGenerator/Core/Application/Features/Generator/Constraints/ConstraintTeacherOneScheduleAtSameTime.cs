using Ladesa.TimetableGenerator.Core.Application.Features.Generator.Core;

namespace Ladesa.TimetableGenerator.Core.Application.Features.Generator.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no more than one schedule at the same time.
/// </summary>
public abstract class ConstraintTeacherOneScheduleAtSameTime : IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
        var allSchedulesProposalsByDateTeacherIdTimeSlot =
            from scheduleProposal in generationContext.AllProposals
            group scheduleProposal by new
            {
                Data = scheduleProposal.Date,
                ProfessorId = scheduleProposal.TeacherId,
                scheduleProposal.TimeSlot
            }
            into schedulesProposalsByDateTeacherIdTimeSlot
            select new
            {
                schedulesProposalsByDateTeacherIdTimeSlot.Key.Data,
                schedulesProposalsByDateTeacherIdTimeSlot.Key.ProfessorId,
                schedulesProposalsByDateTeacherIdTimeSlot.Key.TimeSlot,
                Proposals = schedulesProposalsByDateTeacherIdTimeSlot.AsEnumerable()
            };

        foreach (var schedulesProposalsByDateTeacherIdTimeSlot in allSchedulesProposalsByDateTeacherIdTimeSlot)
        {
            if (schedulesProposalsByDateTeacherIdTimeSlot == null)
                continue;

            var proposalsBoolVars = schedulesProposalsByDateTeacherIdTimeSlot.Proposals
                .Select(proposal => proposal.ModelBoolVar).ToArray();

            if (proposalsBoolVars.Length == 0) continue;
            generationContext.CpModel.AddAtMostOne(proposalsBoolVars);
        }
    }
}