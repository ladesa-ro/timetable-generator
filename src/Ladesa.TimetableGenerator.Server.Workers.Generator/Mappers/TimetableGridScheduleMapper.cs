using Ladesa.TimetableGenerator.Domain.Models.Schedule;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class TimetableGridScheduleMapper
{
    public static Schedule ToCoreDomainEntity(Msg.ScheduleElement dto)
    {
        return new Schedule(
            dto.GroupId,
            dto.DiaryId,
            dto.TeacherId,
            DateOnly.FromDateTime(dto.Date.DateTime),
            TimeSlotMapper.ToCoreDomainValueObject(dto.TimeSlot)
        );
    }

    public static Msg.ScheduleElement ToMessagesDto(Schedule domain)
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
