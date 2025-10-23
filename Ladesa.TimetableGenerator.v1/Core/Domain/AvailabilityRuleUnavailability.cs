using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization.DataTypes;

namespace Ladesa.TimetableGenerator.v1.Core.Domain;

public record AvailabilityRuleUnavailability(
    string RRule,
    DateTime DateStart,
    DateTime? DateEnd
)
{
    private static readonly RecurrencePatternSerializer RecurrencePatternSerializer = new();

    private static RecurrencePattern? ParseRecurrencePattern(string? rrule)
    {
        if(rrule is null || rrule.Length == 0) return null;

        var stringReader = new StringReader(rrule);
        try
        {
            var recurrencePatternDeserialized = RecurrencePatternSerializer.Deserialize(stringReader);
            return recurrencePatternDeserialized switch
            {
                RecurrencePattern recurrencePattern => recurrencePattern,
                _ => null
            };
        }
        catch
        {
            return null;
        }
    }

    public RecurrencePattern? RecurrencePattern { get; } = ParseRecurrencePattern(RRule);

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

    private RecurrencePattern? GetEffectiveRecurrencePattern()
    {
        if (!string.IsNullOrEmpty(RRule) && RecurrencePattern is null)
            throw new Exception("invalid RRULE: unable to parse recurrence pattern.");
        if (RecurrencePattern is null) return null;

        if (RecurrencePattern.Frequency == FrequencyType.Weekly
            && (RecurrencePattern.ByDay is null || RecurrencePattern.ByDay.Count == 0))
        {
            var byDay = DayOfWeekToByDay(DateStart.DayOfWeek);

            var parts = RRule.Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Where(p => !p.StartsWith("BYDAY=", StringComparison.OrdinalIgnoreCase)
                            && !p.StartsWith("INTERVAL=", StringComparison.OrdinalIgnoreCase))
                .ToList();
            parts.Add($"BYDAY={byDay}");
            parts.Add("INTERVAL=1");
            var adjusted = string.Join(';', parts);

            return ParseRecurrencePattern(adjusted);
        }

        return RecurrencePattern;
    }

    /// Checks if the specified time slot on a given date is available, based on the unavailability rule.
    /// <param name="checkDate">The date to check for availability.</param>
    /// <param name="checkTimeSlot">The time slot to check for availability.</param>
    /// <returns>True if the time slot is available on the given date; otherwise, false.</returns>
    public bool IsAvailable(DateOnly checkDate, TimeSlot checkTimeSlot)
    {
        var (checkStart, checkEnd) = checkTimeSlot.GetDateTimeRange(checkDate);

        var calendarEvent = new CalendarEvent()
        {
            DtStart = new CalDateTime(DateStart),
            DtEnd = DateEnd is not null ? new CalDateTime((DateTime)DateEnd) : null,
            RecurrenceRules = GetEffectiveRecurrencePattern() is null ? [] : [GetEffectiveRecurrencePattern()!],
        };
        

        var occurrences = calendarEvent
            .GetOccurrences(new CalDateTime(checkStart.Date))
            .TakeWhileBefore(new CalDateTime(checkEnd));

        var allOccurrencesAvailable = occurrences.All(occurrence =>
        {
            var occurrenceStart = occurrence.Period.StartTime.Value;
            var occurrenceEnd = DateEnd is null
                ? occurrence.Period.EndTime?.Value
                : occurrence.Period.EffectiveEndTime?.Value;

            if (occurrenceEnd is not null)
            {
                if (checkStart < occurrenceEnd && checkEnd > occurrenceStart)
                {
                    return false;
                }
            }
            else
            {
                if (checkStart >= occurrenceStart)
                {
                    return false;
                }
            }

            return true;
        });

        return allOccurrencesAvailable;
    }
};