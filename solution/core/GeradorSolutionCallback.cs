using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Domain;

namespace Ladesa.TimetableGenerator.Core;

public class GeradorSolutionCallback : CpSolverSolutionCallback
{
    public Action<HorarioGerado> Action { get; }
    public GerarHorarioContext Contexto { get; init; }

    public GeradorSolutionCallback(GerarHorarioContext contexto, Action<HorarioGerado> action)
    {
        this.Contexto = contexto;
        this.Action = action;
    }

    public override void OnSolutionCallback()
    {
        var propostasAtivas =
            from propostaAula in this.Contexto.TodasAsPropostasDeAula
            where BooleanValue(propostaAula.ModelBoolVar)
            select new HorarioGeradoAula(
                TurmaId: propostaAula.TurmaId,
                DiarioId: propostaAula.DiarioId,
                ProfessorId: propostaAula.ProfessorId,
                Data: propostaAula.Data,
                IntervaloDeTempo: propostaAula.IntervaloDeTempo
            );

        var horarioGerado = new HorarioGerado(
            DataInicial: this.Contexto.Options.DataInicial,
            DataFinal: this.Contexto.Options.DataFinal,
            HorariosDeAula: this.Contexto.Options.HorariosDeAula,
            Aulas: propostasAtivas.ToArray()
        );

        this.Action(horarioGerado);
    }
}