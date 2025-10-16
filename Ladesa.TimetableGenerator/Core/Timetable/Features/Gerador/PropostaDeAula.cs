using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Features.Gerador;

public class PropostaDeAula(
    GerarHorarioContext contexto,
    string turmaId,
    string diarioId,
    string professorId,
    DateOnly data,
    int intervaloIndex,
    SlotDeTempo slotDeTempo,
    BoolVar? modelBoolVar = null
)
{
    public GerarHorarioContext Contexto { get; set; } = contexto;

    public string TurmaId { get; set; } = turmaId;
    public string DiarioId { get; set; } = diarioId;
    public string ProfessorId { get; set; } = professorId;

    public DateOnly Data { get; set; } = data;
    public int IntervaloIndex { get; set; } = intervaloIndex;

    public SlotDeTempo SlotDeTempo { get; set; } = slotDeTempo;

    private BoolVar? CreatedModelBoolVar { get; set; } = modelBoolVar;

    public BoolVar ModelBoolVar
    {
        get
        {
            if (CreatedModelBoolVar == null)
            {
                var propostaLabel = string.Join(
                    "::",
                    new[]
                    {
                        $"dia_{Data}",
                        $"intervalo_{IntervaloIndex}",
                        $"diario_{DiarioId}",
                        $"turma_{TurmaId}",
                    }
                );

                CreatedModelBoolVar = Contexto.Model.NewBoolVar(propostaLabel);
            }

            return CreatedModelBoolVar!;
        }
        set => CreatedModelBoolVar = value;
    }
}
