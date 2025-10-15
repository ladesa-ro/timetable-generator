using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public class DisciplinaMapper
{
    public static Disciplina ToDomain(DisciplinaDto dto)
    {
        return new Disciplina(Id: dto.Id, Nome: dto.Nome);
    }

    public static DisciplinaDto ToDto(Disciplina domain)
    {
        return new DisciplinaDto()
        {
            Id = domain.Id,
            Nome = domain.Nome,
        };
    }
}
