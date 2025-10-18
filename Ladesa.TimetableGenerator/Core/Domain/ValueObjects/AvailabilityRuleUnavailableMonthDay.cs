namespace Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

public record AvailabilityRuleUnavailableMonthDay(int MonthDay, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableMonthDay;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (date.Day == MonthDay)
            return !TimeSlot.Verify(timeSlot);

        return true;
    }
};