using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.ValueObjects;

public static class WeekDayMapper
{
    public static DayOfWeek ToDomain(WeekDayDto dto)
    {
        return (DayOfWeek)dto;
    }

    public static WeekDayDto ToDto(DayOfWeek domain)
    {
        return (WeekDayDto)domain;
    }
}