using System.Text.Json.Serialization;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

public partial class HorarioGeradoAulaDto
{
    [JsonPropertyName("data")]
    public required DateTimeOffset Data { get; init; }

    [JsonPropertyName("diario_id")]
    public required string DiarioId { get; init; }

    [JsonPropertyName("horario_de_aula")]
    public required SlotDeTempoDto HorarioDeAula { get; init; }

    [JsonPropertyName("professor_id")]
    public required string ProfessorId { get; init; }

    [JsonPropertyName("turma_id")]
    public required string TurmaId { get; init; }
}