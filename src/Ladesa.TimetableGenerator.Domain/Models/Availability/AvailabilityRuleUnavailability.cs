using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;

namespace Ladesa.TimetableGenerator.Domain.Models.Availability;

/// <summary>
///     Represents an unavailability rule defined by a recurrence pattern (RRULE),
///     start date, and optional end date. Pure data record — evaluation logic
///     is handled by <see cref="IAvailabilityEvaluator"/>.
/// </summary>
public record AvailabilityRuleUnavailability(
    string RRule,
    DateTime DateStart,
    DateTime? DateEnd
);
