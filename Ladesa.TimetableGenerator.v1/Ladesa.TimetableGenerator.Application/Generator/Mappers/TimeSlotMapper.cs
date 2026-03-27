namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class TimeSlotMapper
{
    public static Domain.Models.TimeSlot ToCoreDomainValueObject(Msg.TimeSlotElement messagesDto)
    {
        var start = messagesDto.Start.ToString("HH:mm:ss");
        var end = messagesDto.End.ToString("HH:mm:ss");
        var coreDomainValueObject = new Domain.Models.TimeSlot(Start: start, End: end);
        return coreDomainValueObject;
    }

    public static Msg.TimeSlotElement ToMessagesDto(Domain.Models.TimeSlot coreDomainValueObject)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var start = new DateTimeOffset(today.ToDateTime(TimeOnly.Parse(coreDomainValueObject.Start)));
        var end = new DateTimeOffset(today.ToDateTime(TimeOnly.Parse(coreDomainValueObject.End)));

        var messagesDto = new Msg.TimeSlotElement { Start = start, End = end };
        return messagesDto;
    }
}
