namespace Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

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
            return !TimeSlot.Verify(timeSlot);

        return true;
    }
};