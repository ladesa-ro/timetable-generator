using System.Text.Json.Serialization;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

public partial class DisciplinaDto
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("nome")]
    public required string Nome { get; init; }
}
