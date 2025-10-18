using Ladesa.TimetableGenerator.v1.Core.Domain.Entities;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.ValueObjects;
using Ladesa.TimetableGenerator.v1.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.Entities;

public static class TeacherMapper
{
    public static Teacher ToDomain(TeacherDto dto)
    {
        return new Teacher(dto.Id, AvailabilityMapper.ToDomain(dto.Availability));
    }

    public static TeacherDto ToDto(Teacher domain)
    {
        return new TeacherDto
        {
            Id = domain.Id,
            Availability = AvailabilityMapper.ToDto(domain.Availability)
        };
    }
}