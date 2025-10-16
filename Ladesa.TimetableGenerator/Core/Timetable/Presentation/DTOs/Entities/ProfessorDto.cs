using System.Text.Json.Serialization;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

public partial class ProfessorDto
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("regra_disponibilidade")]
    public required IRegraDisponibilidadeDto RegraDisponibilidade { get; init; }
}
