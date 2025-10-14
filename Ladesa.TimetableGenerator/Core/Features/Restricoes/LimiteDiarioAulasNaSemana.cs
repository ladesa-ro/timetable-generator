using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;
using Ladesa.TimetableGenerator.Features.Gerador;

namespace Ladesa.TimetableGenerator.Core.Restricoes;

/// <summary>
///     RESTRIÇÃO: Diário: respeitar limite de quantidade máxima na semana.
/// </summary>
public class LimiteDiarioAulasNaSemana
{
    public static void Aplicar(
        GerarHorarioContext contexto
    )
    {
        foreach (var turma in contexto.Payload.Turmas)
        foreach (var diario in HelperDiarios.ByTurmaId(contexto.Payload, turma.Id))
        {
            var propostasDoDiario =
                from propostaAula in contexto.TodasAsPropostasDeAula
                where propostaAula.DiarioId == diario.Id
                select propostaAula.ModelBoolVar;

            if (propostasDoDiario.Any())
                contexto.Model.Add(
                    LinearExpr.Sum(propostasDoDiario) <= diario.QuantidadeMaximaSemana
                );
        }
    }
}