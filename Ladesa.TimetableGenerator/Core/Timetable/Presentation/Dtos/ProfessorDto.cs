using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

public record ProfessorDto(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("regra_disponibilidade")] RegraDisponibilidadeDto RegraDisponibilidade
);

public static class ProfessorMapper
{
    public static ProfessorDto ToDto(this Professor domain)
        => new(domain.Id, domain.RegraDisponibilidade.ToDto());

    public static Professor ToDomain(this ProfessorDto dto)
        => new(dto.Id, dto.RegraDisponibilidade.ToDomain());
}
