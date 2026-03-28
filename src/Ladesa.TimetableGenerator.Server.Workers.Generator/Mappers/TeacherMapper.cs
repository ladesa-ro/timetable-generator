using Ladesa.TimetableGenerator.Domain.Models.Teacher;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class TeacherMapper
{
    public static Teacher ToCoreDomainEntity(Msg.TeacherElement dto)
        => EntityWithAvailabilityMapper.TeacherToCoreDomainEntity(dto);

    public static Msg.TeacherElement ToMessagesDto(Teacher domain)
        => EntityWithAvailabilityMapper.TeacherToMessagesDto(domain);
}
