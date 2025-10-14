using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

public record GeradorPayloadDto(
    [property: JsonPropertyName("data_inicial")] DateOnly DataInicial,
    [property: JsonPropertyName("data_final")] DateOnly DataFinal,
    [property: JsonPropertyName("turmas")] TurmaDto[] Turmas,
    [property: JsonPropertyName("professores")] ProfessorDto[] Professores,
    [property: JsonPropertyName("diarios")] DiarioDto[] Diarios,
    [property: JsonPropertyName("horarios_de_aula")] SlotDeTempoDto[] HorariosDeAula
);

public static class GeradorPayloadMapper
{
    public static GeradorPayloadDto ToDto(this GeradorPayload domain)
        => new(
            domain.DataInicial,
            domain.DataFinal,
            domain.Turmas.Select(t => t.ToDto()).ToArray(),
            domain.Professores.Select(p => p.ToDto()).ToArray(),
            domain.Diarios.Select(d => d.ToDto()).ToArray(),
            domain.HorariosDeAula.Select(h => h.ToDto()).ToArray()
        );

    public static GeradorPayload ToDomain(this GeradorPayloadDto dto)
        => new(
            dto.DataInicial,
            dto.DataFinal,
            dto.Turmas.Select(t => t.ToDomain()).ToArray(),
            dto.Professores.Select(p => p.ToDomain()).ToArray(),
            dto.Diarios.Select(d => d.ToDomain()).ToArray(),
            dto.HorariosDeAula.Select(h => h.ToDomain()).ToArray()
        );
}
