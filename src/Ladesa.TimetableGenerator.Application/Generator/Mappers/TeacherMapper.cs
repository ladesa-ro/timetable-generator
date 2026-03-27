using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class TeacherMapper
{
    public static Teacher ToCoreDomainEntity(Msg.TeacherElement dto)
        => EntityWithAvailabilityMapper.TeacherToCoreDomainEntity(dto);

    public static Msg.TeacherElement ToMessagesDto(Teacher domain)
        => EntityWithAvailabilityMapper.TeacherToMessagesDto(domain);
}
