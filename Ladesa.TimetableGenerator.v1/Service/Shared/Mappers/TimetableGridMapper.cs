namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class TimetableGridMapper
{
    public static Core.Domain.TimetableGrid ToCoreDomainEntity(Msg.TimeTable messagesDto)
    {
        var coreDomainEntity = new Core.Domain.TimetableGrid(
            DateOnly.FromDateTime(messagesDto.DateStart.DateTime),
            DateOnly.FromDateTime(messagesDto.DateEnd.DateTime),
            messagesDto.TimeSlots?.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray() ?? [],
            messagesDto.Schedules?.Select(TimetableGridScheduleMapper.ToCoreDomainEntity).ToArray() ?? []
        );

        return coreDomainEntity;
    }

    public static Msg.TimeTable ToMessagesDto(Core.Domain.TimetableGrid domain)
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
