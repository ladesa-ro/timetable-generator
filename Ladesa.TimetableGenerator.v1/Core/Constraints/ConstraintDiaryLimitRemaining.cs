using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.v1.Core.Domain;
using Ladesa.TimetableGenerator.v1.Core.Generator;

namespace Ladesa.TimetableGenerator.v1.Core.Constraints;

/// <summary>
///     CONSTRAINT: Diary - limit how many schedules can be in total.
/// </summary>
public abstract class ConstraintDiaryLimitRemaining : IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
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