using System.Text.Json.Serialization;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

public partial class HorarioGeradoDto
{
    [JsonPropertyName("aulas")]
    public required HorarioGeradoAulaDto[] Aulas { get; init; }

    [JsonPropertyName("data_final")]
    public required DateTimeOffset DataFinal { get; init; }

    [JsonPropertyName("data_inicial")]
    public required DateTimeOffset DataInicial { get; init; }

    [JsonPropertyName("horarios_de_aula")]
    public required SlotDeTempoDto[] HorariosDeAula { get; init; }

    [JsonPropertyName("request_id")]
    public required Guid RequestId { get; init; }

    [JsonPropertyName("score")]
    public required int? Score { get; init; }
}
