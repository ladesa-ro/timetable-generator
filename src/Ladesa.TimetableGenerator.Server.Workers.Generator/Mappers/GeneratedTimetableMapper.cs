using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class GeneratedTimetableMapper
{
    public static GenerateTimetableCommandResponse ToCoreDomainEntity(Msg.GeneratedTimetableElement messagesDto)
    {
        var coreDomainEntity = new GenerateTimetableCommandResponse(
            TimetableGridMapper.ToCoreDomainEntity(messagesDto.TimeTable),
            (int)messagesDto.Score
        );

        return coreDomainEntity;
    }

    public static Msg.GeneratedTimetableElement ToMessagesDto(GenerateTimetableCommandResponse coreDomainEntity)
    {
        var messagesDto = new Msg.GeneratedTimetableElement
        {
            TimeTable = TimetableGridMapper.ToMessagesDto(coreDomainEntity.Timetable),
            Score = coreDomainEntity.Score
        };

        return messagesDto;
    }
}
