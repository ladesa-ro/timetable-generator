using Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Core;
using Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no schedules in lunchtime - at least 01:30
/// </summary>
public abstract class ConstraintTeacherLunch : IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
        var before12 = new TimeSlot("11:30:00", "12:00:00");
        var after12 = new TimeSlot("13:00:00", "13:30:00");

        var proposalsByDateAndTeachers =
            from scheduleProposal in generationContext.AllProposals
            where
                before12.Verify(
                    scheduleProposal.TimeSlot.End
                )
                || after12.Verify(
                    scheduleProposal.TimeSlot.Start
                )
            group scheduleProposal by new
            {
                scheduleProposal.Date,
                scheduleProposal.TeacherId
            }
            into groupedScheduleProposalsByDateAndTeacher
            select new
            {
                groupedScheduleProposalsByDateAndTeacher.Key.Date,
                groupedScheduleProposalsByDateAndTeacher.Key.TeacherId,
                Proposals = groupedScheduleProposalsByDateAndTeacher.AsEnumerable()
            };

        foreach (var proposalsByDateAndTeacher in proposalsByDateAndTeachers)
        {
            if (proposalsByDateAndTeacher == null)
                continue;

            var proposalsBoolVars =
                proposalsByDateAndTeacher.Proposals.Select(proposal => proposal.ModelBoolVar).ToArray();

            if (proposalsBoolVars.Length == 0) continue;

            generationContext.CpModel.AddAtMostOne(proposalsBoolVars);
        }
    }
}