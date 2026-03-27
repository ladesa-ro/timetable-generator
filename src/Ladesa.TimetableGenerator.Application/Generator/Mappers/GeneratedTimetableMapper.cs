namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class GeneratedTimetableMapper
{
    public static Domain.Models.GeneratedTimetable ToCoreDomainEntity(Msg.GeneratedTimetableElement messagesDto)
    {
        var coreDomainEntity = new Domain.Models.GeneratedTimetable(
            TimetableGridMapper.ToCoreDomainEntity(messagesDto.TimeTable),
            (int)messagesDto.Score
        );

        return coreDomainEntity;
    }

    public static Msg.GeneratedTimetableElement ToMessagesDto(Domain.Models.GeneratedTimetable coreDomainEntity)
    {
        var messagesDto = new Msg.GeneratedTimetableElement
        {
            TimeTable = TimetableGridMapper.ToMessagesDto(coreDomainEntity.Timetable),
            Score = coreDomainEntity.Score
        };

        return messagesDto;
    }
}
