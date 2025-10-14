using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

public record HorarioGeradoDto(
    [property: JsonPropertyName("data_inicial")] DateOnly DataInicial,
    [property: JsonPropertyName("data_final")] DateOnly DataFinal,
    [property: JsonPropertyName("horarios_de_aula")] SlotDeTempoDto[] HorariosDeAula,
    [property: JsonPropertyName("aulas")] HorarioGeradoAulaDto[] Aulas
);

public static class HorarioGeradoMapper
{
    public static HorarioGeradoDto ToDto(this HorarioGerado domain)
        => new(
            domain.DataInicial,
            domain.DataFinal,
            domain.HorariosDeAula.Select(h => h.ToDto()).ToArray(),
            domain.Aulas.Select(a => a.ToDto()).ToArray()
        );

    public static HorarioGerado ToDomain(this HorarioGeradoDto dto)
        => new(
            dto.DataInicial,
            dto.DataFinal,
            dto.HorariosDeAula.Select(h => h.ToDomain()).ToArray(),
            dto.Aulas.Select(a => a.ToDomain()).ToArray()
        );
}
