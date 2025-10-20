using Ladesa.TimetableGenerator.v1.Core.Domain;
using Ladesa.TimetableGenerator.v1.Core.Generator;

namespace Ladesa.TimetableGenerator.v1.Core.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no more than 12 hours in a day.
/// </summary>
public abstract class ConstraintTeacher12Hours : IGeneratorConstraint
{
    public static void Apply(GenerationContext generationContext)
    {
        var timeSpan12 = new TimeSpan(12, 0, 0);

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
                        && scheduleProposal.TimeSlot.Verify(new TimeSlot("18:00:00", "23:59:59"))
                    select scheduleProposal;

                foreach (var nightProposal in nightProposals)
                {
                    var timeSpanAfter12 = TimeSpan.Parse(nightProposal.TimeSlot.End).Add(timeSpan12);

                    var conflictingProposalsNextDay =
                        from scheduleProposal in generationContext.AllProposals
                        where
                            scheduleProposal.Date == nextDay
                            && scheduleProposal.TeacherId == nightProposal.TeacherId
                            && scheduleProposal.TimeSlot.Verify(timeSpanAfter12)
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