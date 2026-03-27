namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class TimetableGridScheduleMapper
{
    public static Domain.Models.TimetableGridSchedule ToCoreDomainEntity(Msg.ScheduleElement dto)
    {
        return new Domain.Models.TimetableGridSchedule(
            dto.GroupId,
            dto.DiaryId,
            dto.TeacherId,
            DateOnly.FromDateTime(dto.Date.DateTime),
            TimeSlotMapper.ToCoreDomainValueObject(dto.TimeSlot)
        );
    }

    public static Msg.ScheduleElement ToMessagesDto(Domain.Models.TimetableGridSchedule domain)
    {
        return new Msg.ScheduleElement
        {
            GroupId = domain.GroupId,
            DiaryId = domain.DiaryId,
            TeacherId = domain.TeacherId,
            Date = new DateTimeOffset(domain.Date.ToDateTime(TimeOnly.MinValue)),
            TimeSlot = TimeSlotMapper.ToMessagesDto(domain.TimeSlot)
        };
    }
}
