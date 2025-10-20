namespace Ladesa.TimetableGenerator.v1.Core.Domain;

public record Availability(
    AvailabilityRuleUnavailability[]? RulesUnavailability
)
{
    /// <summary>
    /// Determines whether a specific time slot on a given date is available
    /// based on the defined unavailability rules.
    /// </summary>
    /// <param name="checkDate">The date to check for availability.</param>
    /// <param name="checkTimeSlot">The time slot to check for availability.</param>
    /// <returns>True if the specified date and time slot are available; otherwise, false.</returns>
    public bool IsAvailable(DateOnly checkDate, TimeSlot checkTimeSlot)
    {
        if (RulesUnavailability is null or { Length: 0 }) return true;
        
        return RulesUnavailability.All(ruleUnavailability => ruleUnavailability.IsAvailable(checkDate, checkTimeSlot));
    }
};