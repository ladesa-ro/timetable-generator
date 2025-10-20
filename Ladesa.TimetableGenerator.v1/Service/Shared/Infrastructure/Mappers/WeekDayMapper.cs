namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class WeekDayMapper
{
    public static DayOfWeek ToCoreDomainValueObject(Protobuf.WeekDay protobufDto)
    {
        return (DayOfWeek)protobufDto;
    }

    public static Protobuf.WeekDay ToProtobufDto(DayOfWeek domainVo)
    {
        return (Protobuf.WeekDay)domainVo;
    }
}