using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.v1.Core.Domain;

namespace Ladesa.TimetableGenerator.v1.Core.Generator;

public class GeneratorSolutionCallback(
    GenerationContext generationContext,
    Action<GeneratedTimetable> action)
    : CpSolverSolutionCallback
{
    private Action<GeneratedTimetable> Action { get; } = action;
    private GenerationContext GenerationContext { get; } = generationContext;

    public override void OnSolutionCallback()
    {
        var timetableGridSchedules =
            from propostaAula in GenerationContext.AllProposals
            where BooleanValue(propostaAula.ModelBoolVar)
            select new TimetableGridSchedule(
                propostaAula.GroupId,
                propostaAula.DiaryId,
                propostaAula.TeacherId,
                propostaAula.Date,
                propostaAula.TimeSlot
            );


        var timetableGrid = new TimetableGrid(
            GenerationContext.GenerateRequest.DateStart,
            GenerationContext.GenerateRequest.DateEnd,
            GenerationContext.GenerateRequest.TimeSlots,
            timetableGridSchedules.ToArray()
        );

        var scoreValue = (int)ObjectiveValue();


        var generatedTimetable = new GeneratedTimetable(
            timetableGrid,
            scoreValue
        );

        Action(generatedTimetable);
    }
}