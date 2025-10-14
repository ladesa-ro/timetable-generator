using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;
using Ladesa.TimetableGenerator.Core.Timetable.Logic;
using Ladesa.TimetableGenerator.Features.Gerador;

namespace Ladesa.TimetableGenerator.Core.Restricoes;

/// <summary>
///     RESTRIÇÃO: Mínimo de 1h30 de almoço para a turma
/// </summary>
public class LimiteTurmaHorarioAlmoco
{
    public static void Aplicar(GerarHorarioContext contexto)
    {
        var grupos =
            from proposta in contexto.TodasAsPropostasDeAula
            where
                SlotDeTempoEvaluator.VerificarIntervalo(
                    new SlotDeTempo("11:30:00", "12:00:00"),
                    proposta.SlotDeTempo.HorarioFim
                )
                || SlotDeTempoEvaluator.VerificarIntervalo(
                    new SlotDeTempo("13:00:00", "13:30:00"),
                    proposta.SlotDeTempo.HorarioInicio
                )
            group proposta by new { proposta.Data, proposta.TurmaId }
            into variantes
            select new
            {
                variantes.Key.Data,
                variantes.Key.TurmaId,
                Propostas = variantes.AsEnumerable()
            };

        foreach (var grupo in grupos)
        {
            if (grupo == null)
                continue;

            var propostas = grupo.Propostas.Select(Proposta => Proposta.ModelBoolVar).ToList();

            if (propostas.Count != 0)
                contexto.Model.AddAtMostOne(propostas);
        }
    }
}