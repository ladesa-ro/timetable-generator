using Ladesa.TimetableGenerator.Domain.Models.TimeSlot;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class TimeSlotMapper
{
    public static TimeSlot ToCoreDomainValueObject(Msg.TimeSlotElement messagesDto)
    {
        var start = TimeOnly.FromDateTime(messagesDto.Start.DateTime);
        var end = TimeOnly.FromDateTime(messagesDto.End.DateTime);
        return new TimeSlot(start, end);
    }

    public static Msg.TimeSlotElement ToMessagesDto(TimeSlot coreDomainValueObject)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var start = new DateTimeOffset(today.ToDateTime(coreDomainValueObject.Start));
        var end = new DateTimeOffset(today.ToDateTime(coreDomainValueObject.End));

        return new Msg.TimeSlotElement { Start = start, End = end };
    }
}
