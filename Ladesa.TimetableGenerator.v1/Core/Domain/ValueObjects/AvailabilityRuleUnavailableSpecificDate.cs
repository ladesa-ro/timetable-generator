namespace Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

public record AvailabilityRuleUnavailableSpecificDate(DateOnly Date, TimeSlot TimeSlot)
    : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableSpecificDate;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        if (date == Date)
            return !TimeSlot.Verify(timeSlot);

        return true;
    }
};