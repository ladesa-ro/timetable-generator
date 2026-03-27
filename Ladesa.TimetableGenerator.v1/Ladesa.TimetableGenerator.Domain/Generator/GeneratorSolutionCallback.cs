using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Domain.Generator;

public class GeneratorSolutionCallback(
    GenerationContext generationContext,
    Action<GeneratedTimetable> action)
    : CpSolverSolutionCallback
{
    public override void OnSolutionCallback()
    {
        var schedules =
            from proposal in generationContext.AllProposals
            where BooleanValue(proposal.ModelBoolVar)
            select new TimetableGridSchedule(
                proposal.GroupId,
                proposal.DiaryId,
                proposal.TeacherId,
                proposal.Date,
                proposal.TimeSlot
            );

        var timetableGrid = new TimetableGrid(
            generationContext.GenerateRequest.DateStart,
            generationContext.GenerateRequest.DateEnd,
            generationContext.GenerateRequest.TimeSlots,
            schedules.ToArray()
        );

        var generatedTimetable = new GeneratedTimetable(timetableGrid, (int)ObjectiveValue());

        action(generatedTimetable);
    }
}
