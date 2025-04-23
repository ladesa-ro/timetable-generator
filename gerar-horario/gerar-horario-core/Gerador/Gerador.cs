using Google.OrTools.Sat;
using Sisgea.GerarHorario.Core.Dtos.Configuracoes;
using Sisgea.GerarHorario.Core.Dtos.HorarioGerado;

namespace Sisgea.GerarHorario.Core;

public class Gerador
{
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
        Restricoes.AplicarLimiteDeNoMaximoUmDiarioAtivoPorTurmaEmUmHorario(contexto);

        // ======================================
        // RESTRIÇÃO: Professor: não ter mais de uma aula ativa ao mesmo tempo.
        Restricoes.AplicarLimiteDeNoMaximoUmDiarioAtivoPorProfessorEmUmHorario(contexto);

        // ======================================
        // RESTRIÇÃO: Diário: respeitar limite de quantidade máxima na semana.
        Restricoes.AplicarLimiteDeDiarioNaSemana(contexto);

        // ======================================
        //RESTRIÇÃO: Mínimo de 1h30 de almoço para o professor.
        Restricoes.HorarioAlmocoProfessor(contexto);

        // ======================================
        //RESTRIÇÃO: Mínimo de 1h30 de almoço para a turma.
        Restricoes.HorarioAlmocoTurma(contexto);

        // ======================================
        //RESTRIÇÃO: O professor não pode trabalhar 3 turnos e o professor não pode trabalhar de manhã e à noite.
        Restricoes.ProfessorNaoPodeTrabalharEmTresTurnosDiferentes(contexto);

        // ======================================
        // RESTRIÇÃO: A diferença entre os turnos de trabalho do professor deve ser de 12 horas.
        Restricoes.DiferencaTurnos12Horas(contexto);

        // ======================================
        /// RESTRIÇÃO: Permitir escolher dias e turnos de aula de um professor.
        Restricoes.AgruparDisciplinasParametro(contexto, "7", 2, new("13:50", "17:30"));

        // ====================================================================
        // RESTRIÇÃO: Todo professor deve ter 1 dia sem aulas (PRD na segunda ou na sexta).
         Restricoes.PadronizarPRD(contexto);

        // ====================================================================
        // RESTRIÇÃO: Permitir escolher o dia de disponibilidade (PRD) de um professor.
        //Restricoes.EspecificarPRD(contexto, "1", 5);
        




        // Ajudar o modelo para gerar o resultado mais satisfatório dentre
        // todas as soluções possíveis.
        Gerador.OtimizarResultadoDeAcordoComAsPreferencias(contexto);

        // ====================================================================

        return contexto;
    }

    public static IEnumerable<HorarioGerado> GerarHorario(
      GerarHorarioOptions options)
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
                
                var solver = new CpSolver
                {
                    StringParameters = "enumerate_all_solutions:true"
                };

                var solutionPrinter = new GeradorSolutionCallback(contexto, (spHorarioGerado) =>
                {
                    horarioGerado = spHorarioGerado;
                    tickGenerated.Set();
                });

                if (previousScore != null)
                {
                    Gerador.OtimizarResultadoDeAcordoComAsPreferencias(contexto, previousScore - 1);
                }

                var sat = solver.Solve(contexto.Model, solutionPrinter);


                if (sat == CpSolverStatus.Feasible || sat == CpSolverStatus.Optimal)
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
    public static void OtimizarResultadoDeAcordoComAsPreferencias(GerarHorarioContext contexto, long? limiteScore = null)
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

