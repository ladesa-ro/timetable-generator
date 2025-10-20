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

    private static RecurrencePattern ParseRecurrencePattern(string rrule)
    {
        var stringReader = new StringReader(rrule);
        var recurrencePatternDeserialized = RecurrencePatternSerializer.Deserialize(stringReader);

        return recurrencePatternDeserialized switch
        {
            RecurrencePattern recurrencePattern => recurrencePattern,
            _ => throw new Exception("Invalid RRule.")
        };
    }

    public RecurrencePattern RecurrencePattern { get; } = ParseRecurrencePattern(RRule);

    /// Checks if the specified time slot on a given date is available, based on the unavailability rule.
    /// <param name="checkDate">The date to check for availability.</param>
    /// <param name="checkTimeSlot">The time slot to check for availability.</param>
    /// <returns>True if the time slot is available on the given date; otherwise, false.</returns>
    public bool IsAvailable(DateOnly checkDate, TimeSlot checkTimeSlot)
    {
        var calendarEvent = new CalendarEvent()
        {
            DtStart = new CalDateTime(DateStart),
            DtEnd = DateEnd is not null ? new CalDateTime((DateTime)DateEnd) : null,
            RecurrenceRules = [RecurrencePattern],
        };
        
        var (checkStart, checkEnd) = checkTimeSlot.GetDateTimeRange(checkDate);

        var occurrences = calendarEvent
            .GetOccurrences(
                new CalDateTime(checkStart.Date)
            )
            .TakeWhileBefore(
                new CalDateTime(checkEnd.AddDays(1))
            );


        var allOccurrencesAvailable = occurrences.All(occurrence =>
        {
            var occurenceDateStart = occurrence.Period.StartTime.AsUtc;
            var occurenceDateEnd = DateEnd is null ? occurrence.Period.EndTime?.AsUtc : occurrence.Period.EffectiveEndTime?.AsUtc;
            
            if (occurenceDateEnd is not null)
            {
                if (checkStart < occurenceDateEnd && checkEnd > occurenceDateStart)
                {
                    return false;
                }
            }
            else
            {
                if (checkStart >= occurenceDateStart)
                {
                    return false;
                }
            }

            return true;
        });


        return allOccurrencesAvailable;
    }
};