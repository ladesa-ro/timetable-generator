using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no more than 12 hours in a day.
/// </summary>
public class ConstraintTeacher12Hours : IConstraintTeacher12Hours
{
    private static readonly TimeSpan MaxDailyWorkDuration = new(12, 0, 0);

    public void Apply(IGenerationContext context)
    {
        var generationContext = (GenerationContext)context;
        foreach (var date in generationContext.GenerateRequest.GetDates())
        {
            var nextDay = date.AddDays(1);

            foreach (var teacher in generationContext.GenerateRequest.Teachers)
            {
                var nightProposals =
                    from scheduleProposal in generationContext.AllProposals
                    where
                        scheduleProposal.TeacherId == teacher.Id
                        && scheduleProposal.Date == date
                        && scheduleProposal.TimeSlot.Contains(TimeSlotConstants.NightShift)
                    select scheduleProposal;

                foreach (var nightProposal in nightProposals)
                {
                    var timeSpanAfter12 = TimeSpan.Parse(nightProposal.TimeSlot.End).Add(MaxDailyWorkDuration);

                    var conflictingProposalsNextDay =
                        from scheduleProposal in generationContext.AllProposals
                        where
                            scheduleProposal.Date == nextDay
                            && scheduleProposal.TeacherId == nightProposal.TeacherId
                            && scheduleProposal.TimeSlot.Contains(timeSpanAfter12)
                        select scheduleProposal.ModelBoolVar;

                    var proposalsBoolVars = conflictingProposalsNextDay.ToArray();

                    if (proposalsBoolVars.Length == 0) continue;

                    var negatedVariables = proposalsBoolVars
                        .Select(v => v.Not())
                        .ToArray();

                    generationContext
                        .CpModel.AddBoolAnd(negatedVariables)
                        .OnlyEnforceIf(nightProposal.ModelBoolVar);
                }
            }
        }
    }
}
