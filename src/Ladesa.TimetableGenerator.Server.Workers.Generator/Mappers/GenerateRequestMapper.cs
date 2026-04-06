using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Domain.Models.Diary;
using Ladesa.TimetableGenerator.Domain.Models.Group;
using Ladesa.TimetableGenerator.Domain.Models.Teacher;
using Ladesa.TimetableGenerator.Domain.Models.TimeSlot;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class GenerateRequestMapper
{
    public static GenerateTimetableCommand ToCoreDomainEntity(Msg.GenerateRequest dto)
        => MapToCoreDomainEntity(
            dto.DateStart, dto.DateEnd,
            dto.Groups?.Select(GroupMapper.ToCoreDomainEntity).ToArray(),
            dto.Teachers?.Select(TeacherMapper.ToCoreDomainEntity).ToArray(),
            dto.Diaries?.Select(DiaryMapper.ToCoreDomainEntity).ToArray(),
            dto.TimeSlots?.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray(),
            dto.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToCoreDomainEntity(dto.PreviousTimetableGrid)
                : null);

    public static GenerateTimetableCommand ToCoreDomainEntity(Msg.GenerateRequestClass dto)
        => MapToCoreDomainEntity(
            dto.DateStart, dto.DateEnd,
            dto.Groups?.Select(GroupMapper.ToCoreDomainEntity).ToArray(),
            dto.Teachers?.Select(TeacherMapper.ToCoreDomainEntity).ToArray(),
            dto.Diaries?.Select(DiaryMapper.ToCoreDomainEntity).ToArray(),
            dto.TimeSlots?.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray(),
            dto.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToCoreDomainEntity(dto.PreviousTimetableGrid)
                : null);

    private static GenerateTimetableCommand MapToCoreDomainEntity(
        DateTimeOffset dateStart,
        DateTimeOffset dateEnd,
        Group[]? groups,
        Teacher[]? teachers,
        Diary[]? diaries,
        TimeSlot[]? timeSlots,
        TimetableGrid? previousTimetableGrid)
    {
        return new GenerateTimetableCommand
        {
            DateStart = DateOnly.FromDateTime(dateStart.DateTime),
            DateEnd = DateOnly.FromDateTime(dateEnd.DateTime),
            Groups = groups ?? [],
            Teachers = teachers ?? [],
            Diaries = diaries ?? [],
            TimeSlots = timeSlots ?? [],
            PreviousTimetableGrid = previousTimetableGrid
        };
    }

    public static Msg.GenerateRequestClass ToMessagesDto(GenerateTimetableCommand coreDomainEntity)
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
