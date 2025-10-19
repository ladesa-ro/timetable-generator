using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.v1.Core.Application.DTOs.GenerateRequestExtensions;
using Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Core;

namespace Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Constraints;

/// <summary>
///     CONSTRAINT: Diary - limit how many schedules can be in one week.
/// </summary>
public abstract class ConstraintDiaryLimitSchedulesInOneWeek : IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
        foreach (var group in generationContext.GenerateRequest.Groups)
        foreach (var diary in generationContext.GenerateRequest.DiaryFindByGroupId(group.Id))
        {
            var diaryProposals =
                from scheduleProposal in generationContext.AllProposals
                where scheduleProposal.DiaryId == diary.Id
                select scheduleProposal.ModelBoolVar;

            var diaryProposalsArray = diaryProposals.ToArray();

            if (diaryProposalsArray.Length == 0) continue;

            generationContext.CpModel.Add(
                LinearExpr.Sum(diaryProposalsArray) <= diary.WeekLimit
            );
        }
    }
}