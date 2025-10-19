namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class TimeSlotMapper
{
    public static Core.Domain.ValueObjects.TimeSlot ToCoreDomainValueObject(TimeSlot dto)
    {
        return new Core.Domain.ValueObjects.TimeSlot(dto.Start, dto.End);
    }

    public static TimeSlot ToProtobuf(Core.Domain.ValueObjects.TimeSlot domain)
    {
        return new TimeSlot { Start = domain.Start, End = domain.End };
    }
}