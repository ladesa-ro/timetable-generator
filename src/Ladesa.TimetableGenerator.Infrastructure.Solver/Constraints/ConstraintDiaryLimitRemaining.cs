using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Diary - limit how many schedules can be in total.
/// </summary>
public class ConstraintDiaryLimitRemaining : IConstraintDiaryLimitRemaining
{
    public void Apply(IGenerationContext context)
    {
        var generationContext = (GenerationContext)context;
        foreach (var group in generationContext.GenerateRequest.Groups)
        foreach (var diary in generationContext.GenerateRequest.DiaryFindByGroupId(group.Id))
        {
            if (diary.Remaining < 0) continue;

            var diaryProposals =
                (from scheduleProposal in generationContext.AllProposals
                    where scheduleProposal.DiaryId == diary.Id
                    select scheduleProposal.ModelBoolVar).ToArray();

            if (diaryProposals.Length == 0) continue;

            generationContext.CpModel.Add(
                LinearExpr.Sum(diaryProposals) <= diary.Remaining
            );
        }
    }
}
