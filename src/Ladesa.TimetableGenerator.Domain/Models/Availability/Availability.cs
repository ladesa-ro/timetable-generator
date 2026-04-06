using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;

namespace Ladesa.TimetableGenerator.Domain.Models.Availability;

public record Availability(
    AvailabilityRuleUnavailability[]? RulesUnavailability
)
{
    /// <summary>
    /// Determines whether a specific time slot on a given date is available
    /// based on the defined unavailability rules.
    /// </summary>
    public bool IsAvailable(DateOnly checkDate, TimeSlot.TimeSlot checkTimeSlot, IAvailabilityEvaluator evaluator)
    {
        if (RulesUnavailability is null or { Length: 0 }) return true;

        return RulesUnavailability.All(rule => evaluator.IsAvailable(rule, checkDate, checkTimeSlot));
    }
}
