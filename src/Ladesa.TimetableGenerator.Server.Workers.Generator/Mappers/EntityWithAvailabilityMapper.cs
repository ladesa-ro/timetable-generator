using Ladesa.TimetableGenerator.Domain.Models.Group;
using Ladesa.TimetableGenerator.Domain.Models.Teacher;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

/// <summary>
///     Shared mapping logic for entities with Id + Availability (Group, Teacher).
/// </summary>
public static class EntityWithAvailabilityMapper
{
    public static Group GroupToCoreDomainEntity(Msg.GroupElement dto)
    {
        return new Group(
            Id: dto.Id,
            Availability: AvailabilityMapper.ToCoreDomainEntity(dto.Availability)
        );
    }

    public static Msg.GroupElement GroupToMessagesDto(Group domain)
    {
        return new Msg.GroupElement
        {
            Id = domain.Id,
            Availability = AvailabilityMapper.ToMessagesDto(domain.Availability)
        };
    }

    public static Teacher TeacherToCoreDomainEntity(Msg.TeacherElement dto)
    {
        return new Teacher(
            Id: dto.Id,
            Availability: AvailabilityMapper.ToCoreDomainEntity(dto.Availability)
        );
    }

    public static Msg.TeacherElement TeacherToMessagesDto(Teacher domain)
    {
        return new Msg.TeacherElement
        {
            Id = domain.Id,
            Availability = AvailabilityMapper.ToMessagesDto(domain.Availability)
        };
    }
}
