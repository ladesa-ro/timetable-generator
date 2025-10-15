using System.Text.Json.Serialization;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

public partial class SlotDeTempoDto
{
    [JsonPropertyName("fim")]
    public required string Fim { get; init; }

    [JsonPropertyName("inicio")]
    public required string Inicio { get; init; }
}