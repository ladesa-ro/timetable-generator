using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

public record DiarioDto(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("turma_id")] string TurmaId,
    [property: JsonPropertyName("professor_id")] string ProfessorId,
    [property: JsonPropertyName("disciplina_id")] string DisciplinaId,
    [property: JsonPropertyName("quantidade_maxima_semana")] int QuantidadeMaximaSemana,
    [property: JsonPropertyName("quantidade_maxima_total")] int QuantidadeMaximaTotal
);

public static class DiarioMapper
{
    public static DiarioDto ToDto(this Diario domain)
        => new(
            domain.Id,
            domain.TurmaId,
            domain.ProfessorId,
            domain.DisciplinaId,
            domain.QuantidadeMaximaSemana,
            domain.QuantidadeMaximaTotal
        );

    public static Diario ToDomain(this DiarioDto dto)
        => new(
            dto.Id,
            dto.TurmaId,
            dto.ProfessorId,
            dto.DisciplinaId,
            dto.QuantidadeMaximaSemana,
            dto.QuantidadeMaximaTotal
        );
}
