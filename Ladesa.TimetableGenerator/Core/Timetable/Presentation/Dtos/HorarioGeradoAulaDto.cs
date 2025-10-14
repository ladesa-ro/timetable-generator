using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

public record HorarioGeradoAulaDto(
    [property: JsonPropertyName("turma_id")] string TurmaId,
    [property: JsonPropertyName("diario_id")] string DiarioId,
    [property: JsonPropertyName("professor_id")] string ProfessorId,
    [property: JsonPropertyName("data")] DateOnly Data,
    [property: JsonPropertyName("horario_de_aula")] SlotDeTempoDto HorarioDeAula
);

public static class HorarioGeradoAulaMapper
{
    public static HorarioGeradoAulaDto ToDto(this HorarioGeradoAula domain)
        => new(domain.TurmaId, domain.DiarioId, domain.ProfessorId, domain.Data, domain.HorarioDeAula.ToDto());

    public static HorarioGeradoAula ToDomain(this HorarioGeradoAulaDto dto)
        => new(dto.TurmaId, dto.DiarioId, dto.ProfessorId, dto.Data, dto.HorarioDeAula.ToDomain());
}
