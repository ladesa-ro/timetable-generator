using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Features.Gerador;

public class GeradorSolutionCallback : CpSolverSolutionCallback
{
    public GeradorSolutionCallback(GerarHorarioContext contexto, Action<GeneratedTimetable> action)
    {
        Contexto = contexto;
        Action = action;
    }

    public Action<GeneratedTimetable> Action { get; }
    public GerarHorarioContext Contexto { get; init; }

    public override void OnSolutionCallback()
    {
        var propostasAtivas =
            from propostaAula in Contexto.TodasAsPropostasDeAula
            where BooleanValue(propostaAula.ModelBoolVar)
            select new GeneratedTimetableLesson(
                propostaAula.TurmaId,
                propostaAula.DiarioId,
                propostaAula.ProfessorId,
                propostaAula.Data,
                propostaAula.TimeSlot
            );

        var scoreValue = (int)ObjectiveValue();

        var horarioGerado = new GeneratedTimetable(
            Contexto.Payload.RequestId,
            Contexto.Payload.DateStart,
            Contexto.Payload.DateEnd,
            Contexto.Payload.TimeSlots,
            propostasAtivas.ToArray(),
            scoreValue
        );

        Action(horarioGerado);
    }
}