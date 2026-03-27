namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class DiaryMapper
{
    public static Domain.Models.Diary ToCoreDomainEntity(Msg.DiaryElement messagesDto)
    {
        var coreDomainEntity = new Domain.Models.Diary(
            messagesDto.Id,
            messagesDto.GroupId,
            messagesDto.TeacherId,
            messagesDto.SubjectId,
            (int)messagesDto.WeekLimit,
            (int)messagesDto.Remaining
        );

        return coreDomainEntity;
    }

    public static Msg.DiaryElement ToMessagesDto(Domain.Models.Diary coreDomainEntity)
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
