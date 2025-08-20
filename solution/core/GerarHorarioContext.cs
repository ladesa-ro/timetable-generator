using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Domain;

namespace Ladesa.TimetableGenerator.Core;

public class GerarHorarioContext
{
    public GerarHorarioOptions Options { get; init; }
    public CpModel Model { get; init; }
    public List<PropostaDeAula> TodasAsPropostasDeAula { get; init; }

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
        {
            this.IniciarTodasAsPropostasDeAula();
        }
    }

    public void IniciarTodasAsPropostasDeAula()
    {
        this.TodasAsPropostasDeAula.Clear();

        foreach (var combinacao in Gerador.GerarCombinacoesComDisponibilidade(this.Options))
        {
            var intervalo = this.Options.HorarioDeAulaFindByIndexStrict(combinacao.intervaloIndex);

            var propostaDeAula = new PropostaDeAula(
                contexto: this,
                turmaId: combinacao.turmaId,
                diarioId: combinacao.diarioId,
                professorId: combinacao.professorId,
                data: combinacao.diaSemanaIso,
                intervaloIndex: combinacao.intervaloIndex,
                intervaloDeTempo: intervalo
            );

            this.TodasAsPropostasDeAula.Add(propostaDeAula);
        }

        Console.WriteLine($"--> Quantidade de propostas: {this.TodasAsPropostasDeAula.Count}");
    }
}
