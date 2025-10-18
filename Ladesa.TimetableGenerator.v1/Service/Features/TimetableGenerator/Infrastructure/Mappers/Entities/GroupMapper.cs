using Ladesa.TimetableGenerator.v1.Core.Domain.Entities;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.ValueObjects;
using Ladesa.TimetableGenerator.v1.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.Entities;

public static class GroupMapper
{
    public static Group ToDomain(GroupDto dto)
    {
        return new Group(dto.Id, AvailabilityMapper.ToDomain(dto.Availability));
    }

    public static GroupDto ToDto(Group domain)
    {
        return new GroupDto
        {
            Id = domain.Id,
            Availability = AvailabilityMapper.ToDto(domain.AvailabilityRule)
        };
    }
}