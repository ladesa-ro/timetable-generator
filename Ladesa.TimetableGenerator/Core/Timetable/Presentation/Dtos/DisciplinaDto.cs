using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

public record DisciplinaDto(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("nome")] string Nome
);

public static class DisciplinaMapper
{
    public static DisciplinaDto ToDto(this Disciplina domain)
        => new(domain.Id, domain.Nome);

    public static Disciplina ToDomain(this DisciplinaDto dto)
        => new(dto.Id, dto.Nome);
}
