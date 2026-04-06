using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Extensions;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Diary - limit how many schedules can be in total.
/// </summary>
internal class ConstraintDiaryLimitRemaining : IConstraint
{
    public void Apply(GenerationContext context)
    {
        foreach (var group in context.GenerateTimetableCommand.Groups)
        foreach (var diary in context.GenerateTimetableCommand.DiaryFindByGroupId(group.Id))
        {
            if (diary.Remaining < 0) continue;

            var diaryProposals =
                (from scheduleProposal in context.AllProposals
                    where scheduleProposal.DiaryId == diary.Id
                    select scheduleProposal.ModelBoolVar).ToArray();

            if (diaryProposals.Length == 0) continue;

            context.CpModel.Add(
                LinearExpr.Sum(diaryProposals) <= diary.Remaining
            );
        }
    }
}
