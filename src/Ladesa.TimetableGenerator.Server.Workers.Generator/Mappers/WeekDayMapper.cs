namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class WeekDayMapper
{
    public static DayOfWeek ToCoreDomainValueObject(Msg.WeekDayEnum messagesDto)
    {
        return (DayOfWeek)messagesDto;
    }

    public static Msg.WeekDayEnum ToMessagesDto(DayOfWeek domainVo)
    {
        return (Msg.WeekDayEnum)domainVo;
    }
}
