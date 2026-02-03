namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class TimetableGridScheduleMapper
{
    public static Core.Domain.TimetableGridSchedule ToCoreDomainEntity(Msg.ScheduleElement dto)
    {
        return new Core.Domain.TimetableGridSchedule(
            dto.GroupId,
            dto.DiaryId,
            dto.TeacherId,
            DateOnly.FromDateTime(dto.Date.DateTime),
            TimeSlotMapper.ToCoreDomainValueObject(dto.TimeSlot)
        );
    }

    public static Msg.ScheduleElement ToMessagesDto(Core.Domain.TimetableGridSchedule domain)
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
