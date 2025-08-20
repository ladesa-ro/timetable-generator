using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Domain;

namespace Ladesa.TimetableGenerator.Core;

public class GeradorSolutionCallback : CpSolverSolutionCallback
{
    public GeradorSolutionCallback(GerarHorarioContext contexto, Action<HorarioGerado> action)
    {
        Contexto = contexto;
        Action = action;
    }

    public Action<HorarioGerado> Action { get; }
    public GerarHorarioContext Contexto { get; init; }

    public override void OnSolutionCallback()
    {
        var propostasAtivas =
            from propostaAula in Contexto.TodasAsPropostasDeAula
            where BooleanValue(propostaAula.ModelBoolVar)
            select new HorarioGeradoAula(
                propostaAula.TurmaId,
                propostaAula.DiarioId,
                propostaAula.ProfessorId,
                propostaAula.Data,
                propostaAula.IntervaloDeTempo
            );

        var horarioGerado = new HorarioGerado(
            Contexto.Options.DataInicial,
            Contexto.Options.DataFinal,
            Contexto.Options.HorariosDeAula,
            propostasAtivas.ToArray()
        );

        Action(horarioGerado);
    }
}