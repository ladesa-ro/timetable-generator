namespace Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

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

public interface IAvailabilityRule
{
    public AvailabilityType Type { get; }

    public bool Verify(DateOnly date, TimeSlot timeSlot);
}

public record AvailabilityRuleCompound(IAvailabilityRule[] Rules) : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.Compound;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        return Rules.All(compoundRule => compoundRule.Verify(date, timeSlot));
    }
};

public record AvailabilityRuleUnavailableWeekDay(DayOfWeek WeekDay, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableWeekDay;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (WeekDay == date.DayOfWeek) return TimeSlot.Verify(timeSlot);

        return true;
    }
};

public record AvailabilityRuleUnavailableWeekDays(DayOfWeek[] WeekDays, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableWeekDays;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (WeekDays.Contains(date.DayOfWeek)) TimeSlot.Verify(timeSlot);

        return true;
    }
};

public record AvailabilityRuleUnavailableTimeSlot(TimeSlot TimeSlot) : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableTimeSlot;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        return TimeSlot.Verify(timeSlot);
    }
};

public record AvailabilityRuleUnavailableSpecificDate(DateOnly Date, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableSpecificDate;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (date == Date)
            return TimeSlot.Verify(timeSlot);

        return true;
    }
};

public record AvailabilityRuleUnavailableDateRange(
    DateOnly Start,
    DateOnly End,
    TimeSlot TimeSlot
) : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableDateRange;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (date >= Start && date <= End)
            return TimeSlot.Verify(timeSlot);
        return true;
    }
};

public record AvailabilityRuleUnavailableMonthDay(int MonthDay, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableMonthDay;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (date.Day == MonthDay)
            return TimeSlot.Verify(timeSlot);
        return true;
    }
};

public record AvailabilityRuleUnavailableYearMonths(int[] Months, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableYearMonths;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (Months.Contains(date.Month))
            return TimeSlot.Verify(timeSlot);

        return true;
    }
};