namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class GeneratedTimetableMapper
{
    public static Core.Domain.GeneratedTimetable ToCoreDomainEntity(Msg.GeneratedTimetableElement messagesDto)
    {
        var coreDomainEntity = new Core.Domain.GeneratedTimetable(
            TimetableGridMapper.ToCoreDomainEntity(messagesDto.TimeTable),
            (int)messagesDto.Score
        );

        return coreDomainEntity;
    }

    public static Msg.GeneratedTimetableElement ToMessagesDto(Core.Domain.GeneratedTimetable coreDomainEntity)
    {
        var messagesDto = new Msg.GeneratedTimetableElement
        {
            TimeTable = TimetableGridMapper.ToMessagesDto(coreDomainEntity.Timetable),
            Score = coreDomainEntity.Score
        };

        return messagesDto;
    }
}
