using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization.DataTypes;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver;

/// <summary>
///     Evaluates availability rules using Ical.Net for recurrence pattern processing.
/// </summary>
public class IcalAvailabilityEvaluator : IAvailabilityEvaluator
{
    private static readonly RecurrencePatternSerializer RecurrencePatternSerializer = new();

    public bool IsAvailable(AvailabilityRuleUnavailability rule, DateOnly checkDate, TimeSlot checkTimeSlot)
    {
        var (checkStart, checkEnd) = checkTimeSlot.GetDateTimeRange(checkDate);

        var effectivePattern = GetEffectiveRecurrencePattern(rule);

        var calendarEvent = new CalendarEvent
        {
            DtStart = new CalDateTime(rule.DateStart),
            DtEnd = rule.DateEnd is not null ? new CalDateTime((DateTime)rule.DateEnd) : null,
            RecurrenceRules = effectivePattern is null ? [] : [effectivePattern],
        };

        var occurrences = calendarEvent
            .GetOccurrences(new CalDateTime(checkStart.Date))
            .TakeWhileBefore(new CalDateTime(checkEnd));

        return occurrences.All(occ => !HasConflict(checkStart, checkEnd, occ, rule.DateEnd));
    }

    private static RecurrencePattern? ParseRecurrencePattern(string? rrule)
    {
        if (rrule is null || rrule.Length == 0) return null;

        var stringReader = new StringReader(rrule);
        try
        {
            var deserialized = RecurrencePatternSerializer.Deserialize(stringReader);
            return deserialized as RecurrencePattern;
        }
        catch (FormatException)
        {
            return null;
        }
        catch (ArgumentException)
        {
            return null;
        }
    }

    private static RecurrencePattern? GetEffectiveRecurrencePattern(AvailabilityRuleUnavailability rule)
    {
        var pattern = ParseRecurrencePattern(rule.RRule);

        if (!string.IsNullOrEmpty(rule.RRule) && pattern is null)
            throw new GeneratorValidationException(
                GeneratorValidationErrorCode.InvalidRRule,
                "invalid RRULE: unable to parse recurrence pattern.");

        if (pattern is null) return null;

        if (pattern.Frequency == FrequencyType.Weekly
            && (pattern.ByDay is null || pattern.ByDay.Count == 0))
        {
            var byDay = DayOfWeekToByDay(rule.DateStart.DayOfWeek);

            var parts = rule.RRule.Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Where(p => !p.StartsWith("BYDAY=", StringComparison.OrdinalIgnoreCase)
                            && !p.StartsWith("INTERVAL=", StringComparison.OrdinalIgnoreCase))
                .ToList();
            parts.Add($"BYDAY={byDay}");
            parts.Add("INTERVAL=1");
            var adjusted = string.Join(';', parts);

            return ParseRecurrencePattern(adjusted);
        }

        return pattern;
    }

    private static string DayOfWeekToByDay(DayOfWeek dow) => dow switch
    {
        DayOfWeek.Monday => "MO",
        DayOfWeek.Tuesday => "TU",
        DayOfWeek.Wednesday => "WE",
        DayOfWeek.Thursday => "TH",
        DayOfWeek.Friday => "FR",
        DayOfWeek.Saturday => "SA",
        DayOfWeek.Sunday => "SU",
        _ => throw new ArgumentOutOfRangeException(nameof(dow))
    };

    private static bool HasConflict(DateTime checkStart, DateTime checkEnd, Occurrence occurrence, DateTime? ruleEndDate)
    {
        var occurrenceStart = occurrence.Period.StartTime.Value;
        var occurrenceEnd = ruleEndDate is null
            ? occurrence.Period.EndTime?.Value
            : occurrence.Period.EffectiveEndTime?.Value;

        if (occurrenceEnd is not null)
            return checkStart < occurrenceEnd && checkEnd > occurrenceStart;

        return checkStart >= occurrenceStart;
    }
}
