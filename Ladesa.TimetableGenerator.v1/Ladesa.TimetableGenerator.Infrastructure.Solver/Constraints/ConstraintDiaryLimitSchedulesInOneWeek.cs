using System.Globalization;
using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Diary - limit how many schedules can be in one week.
/// </summary>
public static class ConstraintDiaryLimitSchedulesInOneWeek
{
    public static void Apply(GenerationContext generationContext)
    {
        foreach (var group in generationContext.GenerateRequest.Groups)
        foreach (var diary in generationContext.GenerateRequest.DiaryFindByGroupId(group.Id))
        {
            if(diary.WeekLimit < 0) continue;

            var diaryProposalsByWeekOfYearList =
                (from scheduleProposal in generationContext.AllProposals
                    where scheduleProposal.DiaryId == diary.Id
                    group scheduleProposal by CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                        scheduleProposal.Date.ToDateTime(new TimeOnly()),
                        CalendarWeekRule.FirstDay,
                        DayOfWeek.Monday
                    )
                    into grouped
                    select new
                    {
                        WeekOfYear = grouped.Key,
                        Proposals = grouped.AsEnumerable().ToArray()
                    }).ToArray();

            

            if (diaryProposalsByWeekOfYearList.Length == 0) continue;

            foreach (var diaryProposalsByWeekOfYear in diaryProposalsByWeekOfYearList)
            {
                var proposals = diaryProposalsByWeekOfYear.Proposals.Select(proposal => proposal.ModelBoolVar).ToArray();
                
                if (proposals.Length == 0) continue;
                
                generationContext.CpModel.Add(
                    LinearExpr.Sum(proposals) <= diary.WeekLimit
                );
            }
        }
    }
}