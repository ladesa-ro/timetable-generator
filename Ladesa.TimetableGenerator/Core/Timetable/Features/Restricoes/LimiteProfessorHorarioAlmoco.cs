using Ladesa.TimetableGenerator.Core.Timetable.Domain.Logic;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;
using Ladesa.TimetableGenerator.Features.Gerador;

namespace Ladesa.TimetableGenerator.Core.Restricoes;

/// <summary>
///     RESTRIÇÃO: Mínimo de 1h30 de almoço para o professor
/// </summary>
public class LimiteProfessorHorarioAlmoco
{
    public static void Aplicar(GerarHorarioContext contexto)
    {
        var grupos =
            from proposta in contexto.TodasAsPropostasDeAula
            where
                SlotDeTempoEvaluator.VerificarIntervalo(
                    new TimeSlot("11:30:00", "12:00:00"),
                    proposta.TimeSlot.End
                )
                || SlotDeTempoEvaluator.VerificarIntervalo(
                    new TimeSlot("13:00:00", "13:30:00"),
                    proposta.TimeSlot.Start
                )
            group proposta by new { proposta.Data, proposta.ProfessorId }
            into variantes
            select new
            {
                variantes.Key.Data,
                variantes.Key.ProfessorId,
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