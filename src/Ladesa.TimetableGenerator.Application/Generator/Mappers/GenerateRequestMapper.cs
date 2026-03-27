namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class GenerateRequestMapper
{
    public static Domain.Models.GenerateRequest ToCoreDomainEntity(Msg.GenerateRequest dto)
    {
        var coreDomainEntity = new Domain.Models.GenerateRequest(
            DateOnly.FromDateTime(dto.DateStart.DateTime),
            DateOnly.FromDateTime(dto.DateEnd.DateTime),
            dto.Groups?.Select(GroupMapper.ToCoreDomainEntity).ToArray() ?? [],
            dto.Teachers?.Select(TeacherMapper.ToCoreDomainEntity).ToArray() ?? [],
            dto.Diaries?.Select(DiaryMapper.ToCoreDomainEntity).ToArray() ?? [],
            dto.TimeSlots?.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray() ?? [],
            dto.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToCoreDomainEntity(dto.PreviousTimetableGrid)
                : null
        );
        return coreDomainEntity;
    }

    public static Domain.Models.GenerateRequest ToCoreDomainEntity(Msg.GenerateRequestClass dto)
    {
        var coreDomainEntity = new Domain.Models.GenerateRequest(
            DateOnly.FromDateTime(dto.DateStart.DateTime),
            DateOnly.FromDateTime(dto.DateEnd.DateTime),
            dto.Groups?.Select(GroupMapper.ToCoreDomainEntity).ToArray() ?? [],
            dto.Teachers?.Select(TeacherMapper.ToCoreDomainEntity).ToArray() ?? [],
            dto.Diaries?.Select(DiaryMapper.ToCoreDomainEntity).ToArray() ?? [],
            dto.TimeSlots?.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray() ?? [],
            dto.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToCoreDomainEntity(dto.PreviousTimetableGrid)
                : null
        );
        return coreDomainEntity;
    }

    public static Msg.GenerateRequestClass ToMessagesDto(Domain.Models.GenerateRequest coreDomainEntity)
    {
        var dto = new Msg.GenerateRequestClass
        {
            DateStart = new DateTimeOffset(coreDomainEntity.DateStart.ToDateTime(TimeOnly.MinValue)),
            DateEnd = new DateTimeOffset(coreDomainEntity.DateEnd.ToDateTime(TimeOnly.MinValue)),
            Groups = coreDomainEntity.Groups.Select(GroupMapper.ToMessagesDto).ToArray(),
            Teachers = coreDomainEntity.Teachers.Select(TeacherMapper.ToMessagesDto).ToArray(),
            Diaries = coreDomainEntity.Diaries.Select(DiaryMapper.ToMessagesDto).ToArray(),
            TimeSlots = coreDomainEntity.TimeSlots.Select(TimeSlotMapper.ToMessagesDto).ToArray(),
            PreviousTimetableGrid = coreDomainEntity.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToMessagesDto(coreDomainEntity.PreviousTimetableGrid)
                : null
        };

        return dto;
    }
}
