using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Domain;
using Ladesa.TimetableGenerator.Core.Restricoes;
using LinearExpr = Google.OrTools.Sat.LinearExpr;

namespace Ladesa.TimetableGenerator.Core;

using CombinacaoAula = (
    DateOnly Data,
    int IntervaloDeTempoIndex,
    string TurmaId,
    string DiarioId,
    string ProfessorId
);

public class Gerador
{
    ///<summary>
    /// UTILITÁRIO: Gera uma lista com todas as combinações de aula possíveis
    /// sem respeitar nenhum critério.
    ///</summary>
    public static IEnumerable<CombinacaoAula> GerarTodasAsCombinacoesPossiveisInclusiveIndisponiveis(
        GerarHorarioOptions options
    )
    {
        for (
            var data = options.DataInicial;
            data <= options.DataFinal;
            data = data.AddDays(1)
        )
        {
            for (
                var intervaloIndex = 0;
                intervaloIndex < options.HorariosDeAula.Length;
                intervaloIndex++
            )
            {
                foreach (var turma in options.Turmas)
                {
                    foreach (var diario in options.DiariosByTurmaId(turma.Id))
                    {
                        var combinacaoAula = new CombinacaoAula(
                            data,
                            intervaloIndex,
                            turma.Id,
                            diario.Id,
                            diario.ProfessorId
                        );

                        yield return combinacaoAula;
                    }
                }
            }
        }
    }

    ///<summary>
    /// UTILITÁRIO: Gera uma lista com todas as combinações de aula possíveis,
    /// respeitando as disponibilidades da turma e disponibilidades do professor.
    ///</summary>
    public static IEnumerable<CombinacaoAula> GerarCombinacoesComDisponibilidade(
        GerarHorarioOptions options
    )
    {
        var combinacoes = GerarTodasAsCombinacoesPossiveisInclusiveIndisponiveis(
            options
        );

        foreach (
            var combinacao in combinacoes
        )
        {
            // =====================================================================================
            var intervaloDeTempo = options.HorarioDeAulaFindByIndexStrict(combinacao.IntervaloDeTempoIndex);

            var turma = options.TurmaFindByIdStrict(combinacao.TurmaId);
            var diario = options.DiarioFindByIdStrict(combinacao.DiarioId);

            var professor = options.ProfessorFindByIdStrict(
                diario.ProfessorId,
                exceptionContext: $" (diário: {diario.Id}, turma: {turma.Id})"
            )!;

            // =====================================================================================


            var disponivelParaTurma =
                turma.RegraDisponibilidade.VerificarDisponibilidade(combinacao.Data, intervaloDeTempo);

            // ===================================

            var disponivelParaProfessor = professor.RegraDisponibilidade.VerificarDisponibilidade(
                combinacao.Data,
                intervaloDeTempo
            );

            // ===================================

            var disponivel = disponivelParaTurma && disponivelParaProfessor;

            // =====================================================================================

            if (disponivel)
            {
                yield return combinacao;
            }
        }
    }


    ///<summary>
    /// Ponto de partida que inicia, restringe e otimiza o modelo para
    /// solucionar o problema da geração de horário.
    ///</summary>
    public static GerarHorarioContext PrepararModelComRestricoes(GerarHorarioOptions options)
    {
        // ====================================================================
        // contexto.Model -> Google.OrTools.Sat.CpModel;
        // contexto.Options -> GerarHorarioOptions;
        // contexto.TodasAsPropostasDeAula -> List<PropostaDeAula>;
        var contexto = new GerarHorarioContext(options, iniciarTodasAsPropostasDeAula: true);
        // ================================================

        // ====================================================================
        // RESTRIÇÃO: Turma: não ter mais de uma aula ativa ao mesmo tempo.
        LimiteTurmaUmaAulaPorHorario.Aplicar(contexto);

        // ======================================
        // RESTRIÇÃO: Professor: não ter mais de uma aula ativa ao mesmo tempo.
        LimiteProfessorUmaAulaPorHorario.Aplicar(contexto);

        // ======================================
        // RESTRIÇÃO: Diário: respeitar limite de quantidade máxima na semana.
        LimiteDiarioAulasNaSemana.Aplicar(contexto);

        // ======================================
        //RESTRIÇÃO: Mínimo de 1h30 de almoço para o professor.
        LimiteProfessorHorarioAlmoco.Aplicar(contexto);

        // ======================================
        //RESTRIÇÃO: Mínimo de 1h30 de almoço para a turma.
        LimiteTurmaHorarioAlmoco.Aplicar(contexto);

        // ======================================
        //RESTRIÇÃO: O professor não pode trabalhar 3 turnos e o professor não pode trabalhar de manhã e à noite.
        LimiteProfessorTurnosDiferentes.Aplicar(contexto);

        // ======================================
        // RESTRIÇÃO: A diferença entre os turnos de trabalho do professor deve ser de 12 horas.
        LimiteProfessorTurnos12Horas.Aplicar(contexto);

        // ======================================
        /// RESTRIÇÃO: Permitir escolher dias e turnos de aula de um professor.
        //Restricoes.AgruparDisciplinasParametro(contexto, "7", 2, new("13:50", "17:30"));

        // ====================================================================
        // RESTRIÇÃO: Todo professor deve ter 1 dia sem aulas (PRD na segunda ou na sexta).
        //Restricoes.PadronizarPRD(contexto);

        // ====================================================================
        // RESTRIÇÃO: Permitir escolher o dia de disponibilidade (PRD) de um professor.
        //Restricoes.EspecificarPRD(contexto, "1", 5);

        // Ajudar o modelo para gerar o resultado mais satisfatório dentre
        // todas as soluções possíveis.
        OtimizarResultadoDeAcordoComAsPreferencias(contexto);

        // ====================================================================

        return contexto;
    }

    public static IEnumerable<HorarioGerado> GerarHorario(GerarHorarioOptions options)
    {
        // CRIA UM MODELO COM AS RESTRIÇÕES VINDAS DAS OPÇÕES
        var contexto = PrepararModelComRestricoes(options);

        // ==============================================================

        // Gatilho para quando "um horário foi gerado".
        var tickGenerated = new AutoResetEvent(false);

        HorarioGerado? horarioGerado = null;

        // thread de solução de horário para essa requisição
        var solutionGeneratorThread = new Thread(() =>
        {
            long? previousScore = null;

            do
            {
                var solver = new CpSolver { StringParameters = "enumerate_all_solutions:true" };

                var solutionPrinter = new GeradorSolutionCallback(
                    contexto,
                    (spHorarioGerado) =>
                    {
                        horarioGerado = spHorarioGerado;
                        tickGenerated.Set();
                    }
                );

                if (previousScore != null)
                {
                    Gerador.OtimizarResultadoDeAcordoComAsPreferencias(contexto, previousScore - 1);
                }

                var sat = solver.Solve(contexto.Model, solutionPrinter);

                if (sat is CpSolverStatus.Feasible or CpSolverStatus.Optimal)
                {
                    var solverScore = solver.ObjectiveValue;
                    previousScore = (long)solverScore;
                }
                else
                {
                    previousScore = 0;
                }
            } while (previousScore > 0);

            horarioGerado = null;
            tickGenerated.Set();
        });

        solutionGeneratorThread.Start();

        do
        {
            tickGenerated.WaitOne();

            if (horarioGerado != null)
            {
                yield return horarioGerado;
            }
        } while (horarioGerado != null);

        yield break;
    }

    ///<summary>
    /// Visto que podem haver várias soluções válidas possíveis, precisamos
    /// otimizar a resposta para que seja a mais satisfatória possível de
    /// acordo com as preferências de agrupamento da turma e preferências
    /// de cada professor.
    ///</summary>
    public static void OtimizarResultadoDeAcordoComAsPreferencias(
        GerarHorarioContext contexto,
        long? limiteScore = null
    )
    {
        var qualidade = LinearExpr.NewBuilder();

        foreach (var propostaDeAula in contexto.TodasAsPropostasDeAula)
        {
            qualidade.AddTerm((IntVar)propostaDeAula.ModelBoolVar, 1);
        }

        if (limiteScore != null)
        {
            contexto.Model.Add(qualidade <= contexto.Model.NewConstant((long)limiteScore));
        }

        contexto.Model.Maximize(qualidade);
    }
}