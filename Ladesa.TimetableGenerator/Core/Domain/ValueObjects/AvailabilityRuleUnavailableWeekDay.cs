namespace Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

public record AvailabilityRuleUnavailableWeekDay(DayOfWeek WeekDay, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableWeekDay;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (WeekDay == date.DayOfWeek)
        {
            return !TimeSlot.Verify(timeSlot);
        };

        return true;
    }
};