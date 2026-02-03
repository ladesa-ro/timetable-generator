namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class DiaryMapper
{
    public static Core.Domain.Diary ToCoreDomainEntity(Msg.DiaryElement messagesDto)
    {
        var coreDomainEntity = new Core.Domain.Diary(
            messagesDto.Id,
            messagesDto.GroupId,
            messagesDto.TeacherId,
            messagesDto.SubjectId,
            (int)messagesDto.WeekLimit,
            (int)messagesDto.Remaining
        );

        return coreDomainEntity;
    }

    public static Msg.DiaryElement ToMessagesDto(Core.Domain.Diary coreDomainEntity)
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
