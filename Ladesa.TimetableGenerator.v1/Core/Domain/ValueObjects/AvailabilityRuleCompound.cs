namespace Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

public record AvailabilityRuleCompound(IAvailabilityRule[] Rules) : IAvailabilityRule
{
    public AvailabilityType Type => AvailabilityType.Compound;

    public bool Verify(DateOnly date, TimeSlot timeSlot)
    {
        return Rules.All(compoundRule => compoundRule.Verify(date, timeSlot));
    }
};