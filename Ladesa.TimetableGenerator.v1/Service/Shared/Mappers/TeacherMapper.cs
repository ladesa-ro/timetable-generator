namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class TeacherMapper
{
    public static Core.Domain.Teacher ToCoreDomainEntity(Msg.TeacherElement messagesDto)
    {
        var coreDomainEntity = new Core.Domain.Teacher(
            messagesDto.Id,
            AvailabilityMapper.ToCoreDomainEntity(messagesDto.Availability)
        );

        return coreDomainEntity;
    }

    public static Msg.TeacherElement ToMessagesDto(Core.Domain.Teacher coreDomainEntity)
    {
        return new Msg.TeacherElement
        {
            Id = coreDomainEntity.Id,
            Availability = AvailabilityMapper.ToMessagesDto(coreDomainEntity.Availability)
        };
    }
}
