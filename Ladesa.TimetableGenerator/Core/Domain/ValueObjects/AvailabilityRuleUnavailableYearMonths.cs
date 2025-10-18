namespace Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

public record AvailabilityRuleUnavailableYearMonths(int[] Months, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableYearMonths;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (Months.Contains(date.Month))
            return !TimeSlot.Verify(timeSlot);

        return true;
    }
};