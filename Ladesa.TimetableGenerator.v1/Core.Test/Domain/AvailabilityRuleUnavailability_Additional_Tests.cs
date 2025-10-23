using Ladesa.TimetableGenerator.v1.Core.Domain;

namespace Ladesa.TimetableGenerator.Test.Domain;

[TestFixture]
public class AvailabilityRuleUnavailability_Additional_Tests
{
    [Test]
    public void Weekly_Without_BYDAY_ShouldDefaultTo_DateStart_DayOfWeek()
    {
        // DateStart on Monday; RRULE without BYDAY should target Monday by default (and set INTERVAL=1)
        var dateStart = new DateTime(2025, 10, 27, 8, 0, 0); // Monday 08:00
        var rule = new AvailabilityRuleUnavailability(
            RRule: "FREQ=WEEKLY",
            DateStart: dateStart,
            DateEnd: new DateTime(2025, 10, 27, 12, 0, 0)
        );

        var monday = DateOnly.FromDateTime(dateStart);
        var tuesday = monday.AddDays(1);

        var slot = new TimeSlot("09:00:00", "10:00:00");

        Assert.That(rule.IsAvailable(monday, slot), Is.False, "Monday should be blocked by default.");
        Assert.That(rule.IsAvailable(tuesday, slot), Is.True, "Tuesday should be available.");
    }
}