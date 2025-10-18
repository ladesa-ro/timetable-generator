using Ladesa.TimetableGenerator.v1.Core.Domain.Entities;
using Ladesa.TimetableGenerator.v1.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.Entities;

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