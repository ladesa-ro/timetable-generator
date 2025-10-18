namespace Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

public interface IAvailabilityRule
{
    public AvailabilityType Type { get; }

    public bool Verify(DateOnly date, TimeSlot timeSlot);
}