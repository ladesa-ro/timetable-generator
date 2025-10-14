using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Features.Payload;
using Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

namespace Ladesa.TimetableGenerator.Features.Gerador;

public class GerarHorarioContext
{
    public GerarHorarioContext(
        IGeradorPayload payload
    )
    {
        Payload = payload;
        IniciarTodasAsPropostasDeAula();
    }

    public IGeradorPayload Payload { get; init; }
    public CpModel Model { get; init; } = new();
    public List<PropostaDeAula> TodasAsPropostasDeAula { get; init; } = [];

    public void IniciarTodasAsPropostasDeAula()
    {
        TodasAsPropostasDeAula.Clear();

        foreach (var combinacao in Gerador.GerarCombinacoesComDisponibilidade(Payload))
        {
            var intervalo = HelperHorarioDeAula.ByIndexStrict(
                Payload,
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