namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class GenerateRequestMapper
{
    public static Domain.Models.GenerateRequest ToCoreDomainEntity(Msg.GenerateRequest dto)
        => MapToCoreDomainEntity(
            dto.DateStart, dto.DateEnd,
            dto.Groups?.Select(GroupMapper.ToCoreDomainEntity).ToArray(),
            dto.Teachers?.Select(TeacherMapper.ToCoreDomainEntity).ToArray(),
            dto.Diaries?.Select(DiaryMapper.ToCoreDomainEntity).ToArray(),
            dto.TimeSlots?.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray(),
            dto.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToCoreDomainEntity(dto.PreviousTimetableGrid)
                : null);

    public static Domain.Models.GenerateRequest ToCoreDomainEntity(Msg.GenerateRequestClass dto)
        => MapToCoreDomainEntity(
            dto.DateStart, dto.DateEnd,
            dto.Groups?.Select(GroupMapper.ToCoreDomainEntity).ToArray(),
            dto.Teachers?.Select(TeacherMapper.ToCoreDomainEntity).ToArray(),
            dto.Diaries?.Select(DiaryMapper.ToCoreDomainEntity).ToArray(),
            dto.TimeSlots?.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray(),
            dto.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToCoreDomainEntity(dto.PreviousTimetableGrid)
                : null);

    private static Domain.Models.GenerateRequest MapToCoreDomainEntity(
        DateTimeOffset dateStart,
        DateTimeOffset dateEnd,
        Domain.Models.Group[]? groups,
        Domain.Models.Teacher[]? teachers,
        Domain.Models.Diary[]? diaries,
        Domain.Models.TimeSlot[]? timeSlots,
        Domain.Models.TimetableGrid? previousTimetableGrid)
    {
        return new Domain.Models.GenerateRequest(
            DateOnly.FromDateTime(dateStart.DateTime),
            DateOnly.FromDateTime(dateEnd.DateTime),
            groups ?? [],
            teachers ?? [],
            diaries ?? [],
            timeSlots ?? [],
            previousTimetableGrid
        );
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
