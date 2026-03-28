using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Domain.Commands;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

internal class GeneratorSolutionCallback(
    GenerationContext generationContext,
    Action<GenerateTimetableCommandResponse> action)
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
            generationContext.GenerateTimetableCommand.DateStart,
            generationContext.GenerateTimetableCommand.DateEnd,
            generationContext.GenerateTimetableCommand.TimeSlots,
            schedules.ToArray()
        );

        var generatedTimetable = new GenerateTimetableCommandResponse(timetableGrid, (int)ObjectiveValue());

        action(generatedTimetable);
    }
}
