using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

public record TurmaDto(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("regra_disponibilidade")] RegraDisponibilidadeDto RegraDisponibilidade
);

public static class TurmaMapper
{
    public static TurmaDto ToDto(this Turma domain)
        => new(domain.Id, domain.RegraDisponibilidade.ToDto());

    public static Turma ToDomain(this TurmaDto dto)
        => new(dto.Id, dto.RegraDisponibilidade.ToDomain());
}
