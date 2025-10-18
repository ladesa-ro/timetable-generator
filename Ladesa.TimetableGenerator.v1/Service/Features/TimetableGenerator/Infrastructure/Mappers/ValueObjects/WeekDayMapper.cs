using Ladesa.TimetableGenerator.v1.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.ValueObjects;

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