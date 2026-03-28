using Ladesa.TimetableGenerator.Domain.Models.Diary;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class DiaryMapper
{
    public static Diary ToCoreDomainEntity(Msg.DiaryElement messagesDto)
    {
        var coreDomainEntity = new Diary(
            messagesDto.Id,
            messagesDto.GroupId,
            messagesDto.TeacherId,
            messagesDto.SubjectId,
            (int)messagesDto.WeekLimit,
            (int)messagesDto.Remaining
        );

        return coreDomainEntity;
    }

    public static Msg.DiaryElement ToMessagesDto(Diary coreDomainEntity)
    {
        var messagesDto = new Msg.DiaryElement
        {
            Id = coreDomainEntity.Id,
            GroupId = coreDomainEntity.GroupId,
            TeacherId = coreDomainEntity.TeacherId,
            SubjectId = coreDomainEntity.SubjectId,
            WeekLimit = coreDomainEntity.WeekLimit,
            Remaining = coreDomainEntity.Remaining
        };

        return messagesDto;
    }
}
