namespace Ladesa.TimetableGenerator.Core.Restricoes;

///<summary>
/// RESTRIÇÃO: Turma: não ter mais de uma aula ativa ao mesmo tempo.
///</summary>
public class LimiteTurmaUmaAulaPorHorario
{
    public static void Aplicar(
        GerarHorarioContext contexto
    )
    {

        var grupos = from proposta in contexto.TodasAsPropostasDeAula
                     group proposta by new { proposta.Data, proposta.TurmaId, proposta.IntervaloIndex } into variantes
                     select new
                     {
                         Data = variantes.Key.Data,
                         TurmaId = variantes.Key.TurmaId,
                         IntervaloIndex = variantes.Key.IntervaloIndex,
                         Propostas = variantes.AsEnumerable(),
                     };

        foreach (var grupo in grupos)
        {
            if (grupo == null) continue;

            var propostas = grupo.Propostas.Select(Proposta => Proposta.ModelBoolVar).ToList();

            if (propostas.Count != 0)
            {
                contexto.Model.AddAtMostOne(propostas);
            }
        }
    }
}