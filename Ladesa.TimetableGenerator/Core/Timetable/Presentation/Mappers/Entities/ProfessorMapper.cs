using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public static class ProfessorMapper
{
    public static Professor ToDomain(ProfessorDto dto)
    {
        return new Professor(dto.Id, RegraDisponibilidadeMapper.ToDomain(dto.RegraDisponibilidade));
    }

    public static ProfessorDto ToDto(Professor domain)
    {
        return new ProfessorDto()
        {
            Id = domain.Id,
            RegraDisponibilidade = RegraDisponibilidadeMapper.ToDto(domain.RegraDisponibilidade),
        };
    }
}
