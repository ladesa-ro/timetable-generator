using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.ValueObjects;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public static class GroupMapper
{
    public static Group ToDomain(GroupDto dto)
    {
        return new Group(dto.Id, AvailabilityMapper.ToDomain(dto.RegraDisponibilidade));
    }

    public static GroupDto ToDto(Group domain)
    {
        return new GroupDto
        {
            Id = domain.Id,
            RegraDisponibilidade = AvailabilityMapper.ToDto(domain.AvailabilityRule)
        };
    }
}