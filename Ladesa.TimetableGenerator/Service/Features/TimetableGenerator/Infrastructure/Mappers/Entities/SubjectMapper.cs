using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.Entities;

public class SubjectMapper
{
    public static Subject ToDomain(SubjectDto dto)
    {
        return new Subject(dto.Id, dto.Name);
    }

    public static SubjectDto ToDto(Subject domain)
    {
        return new SubjectDto { Id = domain.Id, Name = domain.Name };
    }
}