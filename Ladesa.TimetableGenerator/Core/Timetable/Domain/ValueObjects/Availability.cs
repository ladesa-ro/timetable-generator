namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

public enum AvailabilityType
{
    Compound,
    UnavailableWeekDay,
    UnavailableWeekDays,
    UnavailableTimeSlot,
    UnavailableSpecificDate,
    UnavailableDateRange,
    UnavailableMonthDay,
    UnavailableYearMonths
}

public interface AvailabilityRule
{
}

public record AvailabilityRuleCompound(AvailabilityRule[] Rules) : AvailabilityRule;

public record AvailabilityRuleUnavailableWeekDay(DayOfWeek WeekDay, TimeSlot TimeSlot)
    : AvailabilityRule;

public record AvailabilityRuleUnavailableWeekDays(DayOfWeek[] WeekDays, TimeSlot TimeSlot)
    : AvailabilityRule;

public record AvailabilityRuleUnavailableTimeSlot(TimeSlot TimeSlot) : AvailabilityRule;

public record AvailabilityRuleUnavailableSpecificDate(DateOnly Date, TimeSlot TimeSlot)
    : AvailabilityRule;

public record AvailabilityRuleUnavailableDateRange(
    DateOnly Start,
    DateOnly End,
    TimeSlot TimeSlot
) : AvailabilityRule;

public record AvailabilityRuleUnavailableMonthDay(int MonthDay, TimeSlot TimeSlot)
    : AvailabilityRule;

public record AvailabilityRuleUnavailableYearMonths(int[] Months, TimeSlot TimeSlot)
    : AvailabilityRule;