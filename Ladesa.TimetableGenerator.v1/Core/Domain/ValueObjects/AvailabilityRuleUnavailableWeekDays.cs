namespace Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

public record AvailabilityRuleUnavailableWeekDays(DayOfWeek[] WeekDays, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableWeekDays;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (WeekDays.Contains(date.DayOfWeek)) return !TimeSlot.Verify(timeSlot);

        return true;
    }
};