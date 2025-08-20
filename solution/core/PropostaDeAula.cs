using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Domain;

namespace Ladesa.TimetableGenerator.Core;

public class PropostaDeAula(
    GerarHorarioContext contexto,
    string turmaId,
    string diarioId,
    string professorId,
    DateOnly data,
    int intervaloIndex,
    IntervaloDeTempo intervaloDeTempo,
    BoolVar? modelBoolVar = null
)
{
    public GerarHorarioContext Contexto { get; set; } = contexto;

    public string TurmaId { get; set; } = turmaId;
    public string DiarioId { get; set; } = diarioId;
    public string ProfessorId { get; set; } = professorId;

    public DateOnly Data { get; set; } = data;
    public int IntervaloIndex { get; set; } = intervaloIndex;

    public IntervaloDeTempo IntervaloDeTempo { get; set; } = intervaloDeTempo;
    private BoolVar? CreatedModelBoolVar { get; set; } = modelBoolVar;

    public BoolVar ModelBoolVar
    {
        get
        {
            if (this.CreatedModelBoolVar == null)
            {
                var propostaLabel =
                    $"dia_{this.Data}::intervalo_{this.IntervaloIndex}::diario_{this.DiarioId}::turma_{this.TurmaId}";
                this.CreatedModelBoolVar = this.Contexto.Model.NewBoolVar(propostaLabel);
            }
            return this.CreatedModelBoolVar!;
        }
        set { this.CreatedModelBoolVar = value; }
    }
};
