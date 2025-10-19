namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class WeekDayMapper
{
    public static DayOfWeek ToCoreDomainValueObject(WeekDay dto)
    {
        return (DayOfWeek)dto;
    }

    public static WeekDay ToProtobuf(DayOfWeek domain)
    {
        return (WeekDay)domain;
    }
}