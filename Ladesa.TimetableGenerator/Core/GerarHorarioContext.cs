using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Domain;

namespace Ladesa.TimetableGenerator.Core;

public class GerarHorarioContext
{
    public GerarHorarioContext(
        GerarHorarioOptions options,
        CpModel? model = null,
        List<PropostaDeAula>? todasAsPropostasDeAula = null,
        bool iniciarTodasAsPropostasDeAula = true
    )
    {
        Options = options;
        Model = model ?? new CpModel();
        TodasAsPropostasDeAula = todasAsPropostasDeAula ?? [];

        if (iniciarTodasAsPropostasDeAula)
            IniciarTodasAsPropostasDeAula();
    }

    public GerarHorarioOptions Options { get; init; }
    public CpModel Model { get; init; }
    public List<PropostaDeAula> TodasAsPropostasDeAula { get; init; }

    public void IniciarTodasAsPropostasDeAula()
    {
        TodasAsPropostasDeAula.Clear();

        foreach (var combinacao in Gerador.GerarCombinacoesComDisponibilidade(Options))
        {
            var intervalo = Options.HorarioDeAulaFindByIndexStrict(
                combinacao.IntervaloDeTempoIndex
            );

            var propostaDeAula = new PropostaDeAula(
                this,
                combinacao.TurmaId,
                combinacao.DiarioId,
                combinacao.ProfessorId,
                combinacao.Data,
                combinacao.IntervaloDeTempoIndex,
                intervalo
            );

            TodasAsPropostasDeAula.Add(propostaDeAula);
        }

        Console.WriteLine($"--> Quantidade de propostas: {TodasAsPropostasDeAula.Count}");
    }
}
