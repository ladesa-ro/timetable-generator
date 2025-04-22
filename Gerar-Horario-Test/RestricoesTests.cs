using Sisgea.GerarHorario.Core;
using Sisgea.GerarHorario.Core.Dtos.Entidades;
using Sisgea.GerarHorario.Core.Dtos.HorarioGerado;
using System.Security.Cryptography;

public static class RestricoesTest
{

    //RESTRIÇÃO TEST: O professor não pode trabalhar 3 turnos e o professor não pode trabalhar de manhã e à noite.
    public static void ProfessorNaoPodeTrabalharEmTresTurnosDiferentesTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {
            foreach (var diaSemanaIso in Enumerable.Range(contexto.Options.DiaSemanaInicio, contexto.Options.DiaSemanaFim))
            {
                var propostasManha = from aula in horarioGerado
                                     where aula.ProfessorId == professor.Id
                                     && aula.DiaDaSemanaIso == diaSemanaIso
                                     &&
                                     (
                                         aula.IntervaloDeTempo >= 0 && aula.IntervaloDeTempo <= 4
                                     )
                                     select aula;

                var propostasTarde = from aula in horarioGerado
                                     where aula.ProfessorId == professor.Id
                                     && aula.DiaDaSemanaIso == diaSemanaIso
                                     &&
                                     (
                                         aula.IntervaloDeTempo >= 5 && aula.IntervaloDeTempo <= 9
                                     )
                                     select aula;

                var propostasNoite = from aula in horarioGerado
                                     where aula.ProfessorId == professor.Id
                                     && aula.DiaDaSemanaIso == diaSemanaIso
                                     &&
                                     (
                                         aula.IntervaloDeTempo >= 10 && aula.IntervaloDeTempo <= 14
                                     )
                                     select aula;

                if (propostasManha.Any() && propostasTarde.Any() && propostasNoite.Any())
                {
                    if (propostasManha.Any() && propostasTarde.Any() && propostasNoite.Any())
                    {
                        Assert.Fail("ERROR: PROFESSOR TRABALHA NOS 3 TURNOS");

                    }
                    else if (propostasManha.Any() && propostasNoite.Any())
                    {
                        Assert.Fail("ERROR: PROFESSOR TRABALHA NO TURNO MANHA E NOITE");
                    }

                }
            }
        }

    }

    //RESTRIÇÃO TEST: Mínimo de 1h30 de almoço para o professor e turmas.
    public static void HorarioAlmoçoTurmaTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        foreach (var turma in contexto.Options.Turmas)
        {

            var propostaAulaTurma = from proposta in horarioGerado
                                    where (proposta.DiaDaSemanaIso == 2 || proposta.DiaDaSemanaIso == 4)
                                        && proposta.TurmaId == turma.Id
                                        && (
                                            Intervalo.VerificarIntervalo(
                                                new Intervalo("11:30:00", "11:59:59"),
                                                contexto.Options.HorariosDeAula[proposta.IntervaloDeTempo].HorarioFim
                                            )
                                            || Intervalo.VerificarIntervalo(
                                                new Intervalo("13:00:00", "13:30:00"),
                                                 contexto.Options.HorariosDeAula[proposta.IntervaloDeTempo].HorarioInicio
                                            )
                                        )
                                    select proposta;

            if (propostaAulaTurma.Count() > 2)
            {
                Assert.Fail("ERROR: HORARIO DE ALMOÇO TURMA ERROR");
            }

        }
    }

    public static void MinimoDozeHorasEntreTurnosProfesssorTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        int[] horarioNoite = [10, 11, 12, 13];

        //Console.WriteLine("==========");


        foreach (var professor in contexto.Options.Professores)
        {
            foreach (var diaSemanIso in Enumerable.Range(contexto.Options.DiaSemanaInicio, contexto.Options.DiaSemanaFim - 1))
            {


                var propostaAulaNoiteProfessor = (from proposta in horarioGerado
                                                  where (proposta.DiaDaSemanaIso == diaSemanIso)
                                                  && horarioNoite.Contains(proposta.IntervaloDeTempo)
                                                  && proposta.ProfessorId == professor.Id
                                                  orderby proposta.DiaDaSemanaIso descending
                                                  select proposta).FirstOrDefault();


                if (propostaAulaNoiteProfessor is null)
                {
                    //Console.WriteLine("Professor não deu aula a noite");
                    continue;
                }

                var propostaAulaDiaProfessor = (from proposta in horarioGerado
                                                where (proposta.DiaDaSemanaIso == diaSemanIso + 1)
                                                && proposta.ProfessorId == professor.Id
                                                select proposta).FirstOrDefault();

                if (propostaAulaDiaProfessor is null)
                {
                    //Console.WriteLine("Professor não deu aula no proximo dia");

                    continue;
                }


                if (propostaAulaNoiteProfessor.IntervaloDeTempo - 9 > propostaAulaDiaProfessor.IntervaloDeTempo)
                {
                    Assert.Fail("ERROR: INTERVALO DE 12H NÃO RESPEITADO ERROR");
                }
                /* 
                                Console.WriteLine(propostaAulaNoiteProfessor);
                                Console.WriteLine(propostaAulaDiaProfessor);


                 */



                /* 
                    10 => 1
                    11 => 2
                    12 => 3
                    13 => 4
                 */
            }
           //  Console.WriteLine("----------------------");

        }

    }



    public static void HorarioAlmoçoProfessorTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {

            var propostaAulaProfessor = from proposta in horarioGerado
                                        where (proposta.DiaDaSemanaIso == 2 || proposta.DiaDaSemanaIso == 4)
                                            && proposta.ProfessorId == professor.Id
                                            && (
                                                Intervalo.VerificarIntervalo(
                                                    new Intervalo("11:30:00", "11:59:59"),
                                                    contexto.Options.HorariosDeAula[proposta.IntervaloDeTempo].HorarioFim
                                                )
                                                || Intervalo.VerificarIntervalo(
                                                    new Intervalo("13:00:00", "13:30:00"),
                                                     contexto.Options.HorariosDeAula[proposta.IntervaloDeTempo].HorarioInicio
                                                )
                                            )
                                        select proposta;

            if (propostaAulaProfessor.Count() > 2)
            {
                Assert.Fail("ERROR: HORARIO DE ALMOÇO PROFESSOR ERROR");
            }

        }
    }
 //RESTRIÇÃO: Todo professor deve ter 1 dia sem aulas (PRD na segunda ou na sexta). // Permitir escolher o dia de PRD de um professor.
    public static void PRDTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {
            var propostaAulaProfessor = from proposta in horarioGerado
                                        where proposta.DiaDaSemanaIso == professor.DiaPRD
                                        && proposta.ProfessorId == professor.Id
                                        select proposta;

            if (propostaAulaProfessor.Any())
            {
                Assert.Fail($"ERROR: PROFESSOR TRABALHANDO NO SEU PRD \n Prova: Professor {professor.Id} esta trabalhando no dia {professor.DiaPRD}");
            }
        }
    }

    //RESTRIÇÃO: Permitir escolher dias e turnos de aula de um professor.
    public static void EscolherTurnoProfessorTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {
            if (professor.DiaAulaEscolhido != 0)
            {
                System.Console.WriteLine("Testando o lançamento de aula do professor " + professor.Id);
                var propostaAulaProfessor = from proposta in horarioGerado
                                            where proposta.DiaDaSemanaIso == professor.DiaAulaEscolhido
                                            && proposta.ProfessorId == professor.Id
                                            && (
                                                    Intervalo.VerificarIntervalo(
                                                        new Intervalo(professor.IntervaloEscolhido.HorarioInicio, professor.IntervaloEscolhido.HorarioFim),
                                                        contexto.Options.HorariosDeAula[proposta.IntervaloDeTempo].HorarioFim
                                                    )
                                                )
                                            select proposta;
                if (!propostaAulaProfessor.Any())
                {
                    Assert.Fail($"ERROR: ESCOLHER TURNO PROFESSOR");
                }
            }
        }
    }
}
