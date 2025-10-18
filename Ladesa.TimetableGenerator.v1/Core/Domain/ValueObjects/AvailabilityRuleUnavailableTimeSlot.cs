namespace Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

public record AvailabilityRuleUnavailableTimeSlot(TimeSlot TimeSlot) : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.UnavailableTimeSlot;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        return !TimeSlot.Verify(timeSlot);
    }
};