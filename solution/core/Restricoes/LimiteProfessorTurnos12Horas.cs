namespace Ladesa.TimetableGenerator.Core.Restricoes;

/// <summary>
///     RESTRIÇÃO: Professor: não ter mais de uma aula ativa ao mesmo tempo.
/// </summary>
public class LimiteProfessorTurnos12Horas
{
    public static void Aplicar(
        GerarHorarioContext contexto
    )
    {
        foreach (var data in contexto.Options.Datas())
        foreach (var professor in contexto.Options.Professores)
        {
            var propostasNoite =
                from proposta in contexto.TodasAsPropostasDeAula
                where
                    proposta.ProfessorId == professor.Id
                    && proposta.Data == data
                    && proposta.IntervaloIndex >= 10
                    && proposta.IntervaloIndex <= 14
                select proposta;

            foreach (var propostaNoite in propostasNoite)
            {
                // DIA SEGUIBTE
                var diaSeguinte = data.AddDays(1);

                var propostasConflitantesManhaSeguinte =
                    from proposta in contexto.TodasAsPropostasDeAula
                    where
                        proposta.Data == diaSeguinte
                        && proposta.ProfessorId == propostaNoite.ProfessorId
                        && proposta.IntervaloIndex >= 0
                        && proposta.IntervaloIndex <= 4 //SELECIONA OS INTERVALOS DE 0 A 4
                        && proposta.IntervaloIndex <=
                        propostaNoite.IntervaloIndex -
                        10 //DIMUI 10 DO ULTIMO INTERVALO QUE SERA IGUAL AO INTERVALO QUE DEVERA SER REMOVIDO
                    select proposta.ModelBoolVar;

                var negatedVariables = propostasConflitantesManhaSeguinte
                    .Select(v => v.Not())
                    .ToArray();

                contexto
                    .Model.AddBoolAnd(negatedVariables)
                    .OnlyEnforceIf(propostaNoite.ModelBoolVar);
            }
        }
    }
}