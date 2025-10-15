using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public static class DiarioMapper
{
    public static Diario ToDomain(DiarioDto dto)
    {
        return new Diario(
            Id: dto.Id,
            TurmaId: dto.TurmaId,
            ProfessorId: dto.ProfessorId,
            DisciplinaId: dto.DisciplinaId,
            QuantidadeMaximaSemana: (int)dto.QuantidadeMaximaSemana,
            QuantidadeMaximaTotal: (int)(dto.QuantidadeMaximaTotal ?? 100)
        );
    }

    public static DiarioDto ToDto(Diario domain)
    {
        return new DiarioDto()
        {
            Id = domain.Id,
            TurmaId = domain.TurmaId,
            ProfessorId = domain.ProfessorId,
            DisciplinaId = domain.DisciplinaId,
            QuantidadeMaximaSemana = domain.QuantidadeMaximaSemana,
            QuantidadeMaximaTotal = domain.QuantidadeMaximaTotal
        };
    }
}