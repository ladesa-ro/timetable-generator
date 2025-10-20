namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class TimeSlotMapper
{
    public static Core.Domain.TimeSlot ToCoreDomainValueObject(Protobuf.TimeSlot protobufDto)
    {
        var coreDomainValueObject = new Core.Domain.TimeSlot(Start: protobufDto.Start, End: protobufDto.End);
        return coreDomainValueObject;
    }

    public static Protobuf.TimeSlot ToProtobufDto(Core.Domain.TimeSlot coreDomainValueObject)
    {
        var protobufDto = new Protobuf.TimeSlot { Start = coreDomainValueObject.Start, End = coreDomainValueObject.End };
        return protobufDto;
    }
}