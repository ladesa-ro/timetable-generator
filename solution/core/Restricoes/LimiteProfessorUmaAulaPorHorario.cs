namespace Ladesa.TimetableGenerator.Core.Restricoes;

/// <summary>
///     RESTRIÇÃO: Professor: não ter mais de uma aula ativa ao mesmo tempo.
/// </summary>
public class LimiteProfessorUmaAulaPorHorario
{
    public static void Aplicar(
        GerarHorarioContext contexto
    )
    {
        var grupos = from proposta in contexto.TodasAsPropostasDeAula
            group proposta by new { proposta.Data, proposta.ProfessorId, proposta.IntervaloIndex }
            into variantes
            select new
            {
                variantes.Key.Data,
                variantes.Key.ProfessorId,
                variantes.Key.IntervaloIndex,
                Propostas = variantes.AsEnumerable()
            };

        foreach (var grupo in grupos)
        {
            if (grupo == null) continue;

            var propostas = grupo.Propostas.Select(Proposta => Proposta.ModelBoolVar).ToList();

            if (propostas.Count != 0) contexto.Model.AddAtMostOne(propostas);
        }
    }
}