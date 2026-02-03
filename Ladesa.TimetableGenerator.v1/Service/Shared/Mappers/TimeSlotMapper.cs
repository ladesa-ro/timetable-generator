namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class TimeSlotMapper
{
    public static Core.Domain.TimeSlot ToCoreDomainValueObject(Msg.TimeSlotElement messagesDto)
    {
        var start = messagesDto.Start.ToString("HH:mm:ss");
        var end = messagesDto.End.ToString("HH:mm:ss");
        var coreDomainValueObject = new Core.Domain.TimeSlot(Start: start, End: end);
        return coreDomainValueObject;
    }

    public static Msg.TimeSlotElement ToMessagesDto(Core.Domain.TimeSlot coreDomainValueObject)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var start = new DateTimeOffset(today.ToDateTime(TimeOnly.Parse(coreDomainValueObject.Start)));
        var end = new DateTimeOffset(today.ToDateTime(TimeOnly.Parse(coreDomainValueObject.End)));

        var messagesDto = new Msg.TimeSlotElement { Start = start, End = end };
        return messagesDto;
    }
}
