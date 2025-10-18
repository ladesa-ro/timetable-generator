using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Application.DTOs.GenerateRequest;
using Ladesa.TimetableGenerator.Core.Application.Features.Generator.Core;

namespace Ladesa.TimetableGenerator.Core.Application.Features.Generator.Constraints;

/// <summary>
///     CONSTRAINT: Diary - limit how many schedules can be in one week.
/// </summary>
public class ConstraintDiaryLimitSchedulesInOneWeek: IGeneratorConstraint
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

            if (diaryProposals.Any())
                generationContext.CpModel.Add(
                    LinearExpr.Sum(diaryProposals) <= diary.WeekLimit
                );
        }
    }
}