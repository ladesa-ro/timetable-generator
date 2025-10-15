using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public static class TurmaMapper
{
    public static Turma ToDomain(TurmaDto dto)
    {
        return new Turma(dto.Id, RegraDisponibilidadeMapper.ToDomain(dto.RegraDisponibilidade));
    }

    public static TurmaDto ToDto(Turma domain)
    {
        return new TurmaDto()
        {
            Id = domain.Id,
            RegraDisponibilidade = RegraDisponibilidadeMapper.ToDto(domain.RegraDisponibilidade)
        };
    }
}