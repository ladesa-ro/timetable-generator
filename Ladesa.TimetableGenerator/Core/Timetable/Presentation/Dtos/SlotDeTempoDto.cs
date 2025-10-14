using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

public record SlotDeTempoDto(
    [property: JsonPropertyName("horario_inicio")] string HorarioInicio,
    [property: JsonPropertyName("horario_fim")] string HorarioFim
);

public static class SlotDeTempoMapper
{
    public static SlotDeTempoDto ToDto(this SlotDeTempo domain)
        => new(domain.HorarioInicio, domain.HorarioFim);

    public static SlotDeTempo ToDomain(this SlotDeTempoDto dto)
        => new(dto.HorarioInicio, dto.HorarioFim);
}