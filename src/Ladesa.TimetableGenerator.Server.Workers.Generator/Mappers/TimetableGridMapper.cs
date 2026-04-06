using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class TimetableGridMapper
{
    public static TimetableGrid ToCoreDomainEntity(Msg.TimeTable messagesDto)
    {
        var coreDomainEntity = new TimetableGrid(
            DateOnly.FromDateTime(messagesDto.DateStart.DateTime),
            DateOnly.FromDateTime(messagesDto.DateEnd.DateTime),
            messagesDto.TimeSlots?.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray() ?? [],
            messagesDto.Schedules?.Select(TimetableGridScheduleMapper.ToCoreDomainEntity).ToArray() ?? []
        );

        return coreDomainEntity;
    }

    public static Msg.TimeTable ToMessagesDto(TimetableGrid domain)
    {
        var dto = new Msg.TimeTable
        {
            DateStart = new DateTimeOffset(domain.DateStart.ToDateTime(TimeOnly.MinValue)),
            DateEnd = new DateTimeOffset(domain.DateEnd.ToDateTime(TimeOnly.MinValue)),
            TimeSlots = domain.TimeSlots.Select(TimeSlotMapper.ToMessagesDto).ToArray(),
            Schedules = domain.Schedules.Select(TimetableGridScheduleMapper.ToMessagesDto).ToArray()
        };

        return dto;
    }
}
