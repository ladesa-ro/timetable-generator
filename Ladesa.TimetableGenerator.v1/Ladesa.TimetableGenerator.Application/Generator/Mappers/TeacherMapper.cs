namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class TeacherMapper
{
    public static Domain.Models.Teacher ToCoreDomainEntity(Msg.TeacherElement messagesDto)
    {
        var coreDomainEntity = new Domain.Models.Teacher(
            messagesDto.Id,
            AvailabilityMapper.ToCoreDomainEntity(messagesDto.Availability)
        );

        return coreDomainEntity;
    }

    public static Msg.TeacherElement ToMessagesDto(Domain.Models.Teacher coreDomainEntity)
    {
        return new Msg.TeacherElement
        {
            Id = coreDomainEntity.Id,
            Availability = AvailabilityMapper.ToMessagesDto(coreDomainEntity.Availability)
        };
    }
}
