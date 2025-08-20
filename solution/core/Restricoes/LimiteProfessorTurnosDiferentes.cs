using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Domain;

namespace Ladesa.TimetableGenerator.Core.Restricoes;

///<summary>
/// RESTRIÇÃO: Professor: não ter mais de uma aula ativa ao mesmo tempo.
///</summary>
public class LimiteProfessorTurnosDiferentes
{
    public static void Aplicar(
        GerarHorarioContext contexto
    )
    {
        // Propostas de aula agrupadas por Professor e Data
        var propostasAgrupadas = from proposta in contexto.TodasAsPropostasDeAula
                                 group proposta by new { proposta.ProfessorId, proposta.Data }
            into variantes
                                 select new
                                 {
                                     ProfessorId = variantes.Key.ProfessorId,
                                     Data = variantes.Key.Data,
                                     Propostas = variantes.AsEnumerable(),
                                 };

        foreach (var grupo in propostasAgrupadas)
        {
            if (grupo == null) continue;

            var propostas = grupo.Propostas.ToList();

            if (propostas.Count == 0) continue;

            var propostasManha =
                (from proposta in propostas
                 where
                     IntervaloDeTempo.VerificarIntervalo(
                         contexto.Options.HorarioDeAulaFindByIndexStrict(proposta.IntervaloIndex),
                         new IntervaloDeTempo("00:00:00", "11:59:59"))
                 select proposta.ModelBoolVar).ToList();

            var propostasTarde =
                (from proposta in propostas
                 where
                     IntervaloDeTempo.VerificarIntervalo(
                         contexto.Options.HorarioDeAulaFindByIndexStrict(proposta.IntervaloIndex),
                         new IntervaloDeTempo("12:00:00", "17:59:59"))
                 select proposta.ModelBoolVar).ToList();

            var propostasNoite =
                (from proposta in propostas
                 where
                     IntervaloDeTempo.VerificarIntervalo(
                         contexto.Options.HorarioDeAulaFindByIndexStrict(proposta.IntervaloIndex),
                         new IntervaloDeTempo("18:00:00", "23:59:59"))
                 select proposta.ModelBoolVar
                ).ToList();

            /*
            Possibilidades

            | descricao            | manha | tarde | noite |
            | -------------------- | ----- | ----- | ----- |
            | nao dar aula no dia  | false | false | false |
            | dar aula so de MANHA |  true | false | false |
            |  dar aula so a tarde | false |  true | false |
            |  dar aula so a noite | false | false |  true |
            |       manha e tarde  |  true |  true | false |
            |       tarde e noite  | false |  true |  true |
            */
            if (propostasManha.Count == 0 || propostasTarde.Count == 0 || propostasNoite.Count == 0) continue;

            //Console.WriteLine("toppp");
            long[,] possibilidadesPermitidas =
            {
                { 0, 0, 0 }, // nao dar aula no dia
                { 1, 0, 0 }, //dar aula so de MANHA
                { 0, 1, 0 }, //dar aula so a tarde
                { 0, 0, 1 }, //dar aula so a noite
                { 1, 1, 0 }, //manha e tarde
                { 0, 1, 1 }, //tarde e noite
            };

            var prefixo = $"{grupo.ProfessorId}_{grupo.Data.ToString()}";

            var qntAulasManha = contexto.Model.NewIntVar(
                0,
                propostasManha.Count(),
                $"{prefixo}_Manha_QuantidadeAtivos"
            );
            var qntAulasTarde = contexto.Model.NewIntVar(
                0,
                propostasTarde.Count(),
                $"{prefixo}_Tarde_QuantidadeAtivos"
            );
            var qntAulasNoite = contexto.Model.NewIntVar(
                0,
                propostasNoite.Count(),
                $"{prefixo}_Noite_QuantidadeAtivos"
            );

            contexto.Model.Add(qntAulasManha == LinearExpr.Sum(propostasManha));
            contexto.Model.Add(qntAulasTarde == LinearExpr.Sum(propostasTarde));
            contexto.Model.Add(qntAulasNoite == LinearExpr.Sum(propostasNoite));

            var algumaAulaManha = contexto.Model.NewBoolVar(
                $"{prefixo}_Manha_Ativo"
            );
            var algumaAulaTarde = contexto.Model.NewBoolVar(
                $"{prefixo}_Tarde_Ativo"
            );
            var algumaAulaNoite = contexto.Model.NewBoolVar(
                $"{prefixo}_Noite_Ativo"
            );

            contexto.Model.Add(qntAulasManha >= 1).OnlyEnforceIf(algumaAulaManha);
            contexto.Model.Add(qntAulasTarde >= 1).OnlyEnforceIf(algumaAulaTarde);
            contexto.Model.Add(qntAulasNoite >= 1).OnlyEnforceIf(algumaAulaNoite);

            contexto.Model.Add(qntAulasManha < 1).OnlyEnforceIf(algumaAulaManha.Not());
            contexto.Model.Add(qntAulasTarde < 1).OnlyEnforceIf(algumaAulaTarde.Not());
            contexto.Model.Add(qntAulasNoite < 1).OnlyEnforceIf(algumaAulaNoite.Not());

            contexto
                .Model.AddAllowedAssignments(
                    [algumaAulaManha, algumaAulaTarde, algumaAulaNoite]
                )
                .AddTuples(possibilidadesPermitidas);
        }
    }
}