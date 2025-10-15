using System.Text.Json.Serialization;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

public partial class DiarioDto
{
    [JsonPropertyName("disciplina_id")]
    public required string DisciplinaId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("professor_id")]
    public required string ProfessorId { get; init; }

    [JsonPropertyName("quantidade_maxima_semana")]
    public required int QuantidadeMaximaSemana { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("quantidade_maxima_total")]
    public required int? QuantidadeMaximaTotal { get; init; }

    [JsonPropertyName("turma_id")]
    public required string TurmaId { get; init; }
}
