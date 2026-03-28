using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Infrastructure.Solver;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Availability;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.TimeSlot;
using Microsoft.Extensions.Logging.Abstractions;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Test;

[TestFixture]
public class AvailabilityRuleUnavailabilityDetailedTests
{
    private readonly IAvailabilityEvaluator _evaluator = new IcalAvailabilityEvaluator();

    [Test]
    public void AllDayEveryDay_ShouldBlockAllDay()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 0, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 23, minute: 59, second: 59)
        );

        var date = DateOnly.FromDateTime(dateTime: unavailability.DateStart);

        // Within the period
        var slotInside = new TimeSlot(new TimeOnly(8, 0, 0), new TimeOnly(10, 0, 0));
        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, slotInside), expression: Is.False);

        // Before the period
        var slotBefore = new TimeSlot(new TimeOnly(0, 0, 0), new TimeOnly(0, 30, 0));
        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, slotBefore), expression: Is.False);

        // After the period
        var slotAfter = new TimeSlot(new TimeOnly(23, 0, 0), new TimeOnly(23, 59, 0));
        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, slotAfter), expression: Is.False);
    }

    [Test]
    public void EveryMorning_ShouldBlockMorningOnly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 8, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var date = DateOnly.FromDateTime(dateTime: unavailability.DateStart);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(8, 30, 0), new TimeOnly(9, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(7, 0, 0), new TimeOnly(7, 50, 0))), expression: Is.True);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(12, 30, 0), new TimeOnly(13, 30, 0))), expression: Is.True);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(11, 50, 0), new TimeOnly(12, 10, 0))), expression: Is.False);
        });
    }

    [Test]
    public void EveryWednesdayAfternoon_ShouldBlockOnlyOnWednesday()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=WEEKLY;BYDAY=WE",
            DateStart: new DateTime(year: 2025, month: 10, day: 22, hour: 14, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 22, hour: 18, minute: 0, second: 0)
        );

        var wednesday = new DateOnly(year: 2025, month: 10, day: 22); // Wednesday
        var thursday = new DateOnly(year: 2025, month: 10, day: 23); // Thursday

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, wednesday, new TimeSlot(new TimeOnly(15, 0, 0), new TimeOnly(16, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, thursday, new TimeSlot(new TimeOnly(15, 0, 0), new TimeOnly(16, 0, 0))), expression: Is.True);
        });
    }

    [Test]
    public void EventWithoutEndDate_ShouldBlockFromTimeOnwardsForever()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0),
            DateEnd: null
        );

        var date = new DateOnly(year: 2025, month: 10, day: 21);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(11, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(12, 0, 0), new TimeOnly(13, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(9, 30, 0))), expression: Is.True);
        });
    }

    [Test]
    public void MonthlyEvent_OnFirstDay_ShouldBlockCorrectly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=MONTHLY;BYMONTHDAY=1",
            DateStart: new DateTime(year: 2025, month: 11, day: 1, hour: 8, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 11, day: 1, hour: 12, minute: 0, second: 0)
        );

        var firstOfMonth = new DateOnly(year: 2025, month: 11, day: 1);
        var secondOfMonth = new DateOnly(year: 2025, month: 11, day: 2);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, firstOfMonth, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(10, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, secondOfMonth, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(10, 0, 0))), expression: Is.True);
        });
    }

    [Test]
    public void LastFridayOfMonth_ShouldBlockCorrectFridayOnly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=MONTHLY;BYDAY=FR;BYSETPOS=-1",
            DateStart: new DateTime(year: 2025, month: 10, day: 31, hour: 15, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 31, hour: 18, minute: 0, second: 0)
        );

        var lastFriday = new DateOnly(year: 2025, month: 10, day: 31);
        var otherFriday = new DateOnly(year: 2025, month: 10, day: 24);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, lastFriday, new TimeSlot(new TimeOnly(16, 0, 0), new TimeOnly(17, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, otherFriday, new TimeSlot(new TimeOnly(16, 0, 0), new TimeOnly(17, 0, 0))), expression: Is.True);
        });
    }

    [Test]
    public void EveryOtherDay_ShouldBlockEveryOtherDay()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY;INTERVAL=2",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 9, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 17, minute: 0, second: 0)
        );

        var firstDay = new DateOnly(year: 2025, month: 10, day: 20);
        var secondDay = new DateOnly(year: 2025, month: 10, day: 21);
        var thirdDay = new DateOnly(year: 2025, month: 10, day: 22);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, firstDay, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(11, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, secondDay, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(11, 0, 0))), expression: Is.True);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, thirdDay, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(11, 0, 0))), expression: Is.False);
        });
    }

    // Additional 20 test cases below

    [Test]
    public void SingleEvent_ShouldBlockOnlyOnStartDate()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "", // Assuming empty RRULE means single event
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 9, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var startDate = new DateOnly(year: 2025, month: 10, day: 20);
        var nextDate = new DateOnly(year: 2025, month: 10, day: 21);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, startDate, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(11, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, nextDate, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(11, 0, 0))), expression: Is.True);
        });
    }

    [Test]
    public void OverlappingSlot_StartsBeforeEndsDuring_ShouldBlock()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var date = DateOnly.FromDateTime(dateTime: unavailability.DateStart);
        var overlappingSlot = new TimeSlot(new TimeOnly(9, 30, 0), new TimeOnly(10, 30, 0));

        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, overlappingSlot), expression: Is.False);
    }

    [Test]
    public void OverlappingSlot_StartsDuringEndsAfter_ShouldBlock()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var date = DateOnly.FromDateTime(dateTime: unavailability.DateStart);
        var overlappingSlot = new TimeSlot(new TimeOnly(11, 30, 0), new TimeOnly(12, 30, 0));

        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, overlappingSlot), expression: Is.False);
    }

    [Test]
    public void SlotTouchingStart_EndsAtStart_ShouldBeAvailable() // Assuming no overlap if ends exactly at start
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var date = DateOnly.FromDateTime(dateTime: unavailability.DateStart);
        var touchingSlot = new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(10, 0, 0));

        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, touchingSlot), expression: Is.True);
    }

    [Test]
    public void SlotTouchingEnd_StartsAtEnd_ShouldBeAvailable() // Assuming no overlap if starts exactly at end
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var date = DateOnly.FromDateTime(dateTime: unavailability.DateStart);
        var touchingSlot = new TimeSlot(new TimeOnly(12, 0, 0), new TimeOnly(13, 0, 0));

        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, touchingSlot), expression: Is.True);
    }

    [Test]
    public void WeeklyWithMultipleDays_ShouldBlockOnSpecifiedDays()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=WEEKLY;BYDAY=MO,WE,FR",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 13, minute: 0, second: 0), // Monday
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 15, minute: 0, second: 0)
        );

        var monday = new DateOnly(year: 2025, month: 10, day: 20);
        var tuesday = new DateOnly(year: 2025, month: 10, day: 21);
        var wednesday = new DateOnly(year: 2025, month: 10, day: 22);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, monday, new TimeSlot(new TimeOnly(14, 0, 0), new TimeOnly(14, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, tuesday, new TimeSlot(new TimeOnly(14, 0, 0), new TimeOnly(14, 30, 0))), expression: Is.True);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, wednesday, new TimeSlot(new TimeOnly(14, 0, 0), new TimeOnly(14, 30, 0))), expression: Is.False);
        });
    }

    [Test]
    public void MonthlyByDayOfWeek_SecondTuesday_ShouldBlockCorrectly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=MONTHLY;BYDAY=TU;BYSETPOS=2",
            DateStart: new DateTime(year: 2025, month: 10, day: 14, hour: 9, minute: 0, second: 0), // Second Tuesday in Oct 2025
            DateEnd: new DateTime(year: 2025, month: 10, day: 14, hour: 11, minute: 0, second: 0)
        );

        var secondTuesday = new DateOnly(year: 2025, month: 10, day: 14);
        var firstTuesday = new DateOnly(year: 2025, month: 10, day: 7);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, secondTuesday, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(10, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, firstTuesday, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(10, 30, 0))), expression: Is.True);
        });
    }

    [Test]
    public void YearlyEvent_ShouldBlockOnAnniversary()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=YEARLY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 8, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 17, minute: 0, second: 0)
        );

        var sameDayThisYear = new DateOnly(year: 2025, month: 10, day: 20);
        var sameDayNextYear = new DateOnly(year: 2026, month: 10, day: 20);
        var differentDay = new DateOnly(year: 2025, month: 10, day: 21);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, sameDayThisYear, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(10, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, sameDayNextYear, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(10, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, differentDay, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(10, 0, 0))), expression: Is.True);
        });
    }

    [Test]
    public void WithUntilDate_ShouldBlockOnlyBeforeUntil()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY;UNTIL=20251025T120000",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var beforeUntil = new DateOnly(year: 2025, month: 10, day: 24);
        var onUntil = new DateOnly(year: 2025, month: 10, day: 25);
        var afterUntil = new DateOnly(year: 2025, month: 10, day: 26);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, beforeUntil, new TimeSlot(new TimeOnly(11, 0, 0), new TimeOnly(11, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, onUntil, new TimeSlot(new TimeOnly(11, 0, 0), new TimeOnly(11, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, afterUntil, new TimeSlot(new TimeOnly(11, 0, 0), new TimeOnly(11, 30, 0))), expression: Is.True);
        });
    }

    [Test]
    public void WithCount_ShouldBlockOnlyForSpecifiedOccurrences()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY;COUNT=3",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 14, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 16, minute: 0, second: 0)
        );

        var firstDay = new DateOnly(year: 2025, month: 10, day: 20);
        var secondDay = new DateOnly(year: 2025, month: 10, day: 21);
        var thirdDay = new DateOnly(year: 2025, month: 10, day: 22);
        var fourthDay = new DateOnly(year: 2025, month: 10, day: 23);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, firstDay, new TimeSlot(new TimeOnly(15, 0, 0), new TimeOnly(15, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, secondDay, new TimeSlot(new TimeOnly(15, 0, 0), new TimeOnly(15, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, thirdDay, new TimeSlot(new TimeOnly(15, 0, 0), new TimeOnly(15, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, fourthDay, new TimeSlot(new TimeOnly(15, 0, 0), new TimeOnly(15, 30, 0))), expression: Is.True);
        });
    }

    [Test]
    public void EveryTwoWeeks_ShouldBlockAccordingly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=WEEKLY;INTERVAL=2",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 9, minute: 0, second: 0), // Monday
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 11, minute: 0, second: 0)
        );


        var week1 = new DateOnly(year: 2025, month: 10, day: 20);  // Monday, occurrence 1
        var week2 = new DateOnly(year: 2025, month: 10, day: 27);  // Monday, 1 week later (skipped by INTERVAL=2)
        var week3 = new DateOnly(year: 2025, month: 11, day: 3);   // Monday, 2 weeks later (occurrence 2)
        var between = new DateOnly(year: 2025, month: 10, day: 24); // Friday, non-occurrence

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, week1, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(10, 30, 0))), expression: Is.False, () => "Week 1 should be blocked");
            Assert.That(actual: _evaluator.IsAvailable(unavailability, between, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(10, 30, 0))), expression: Is.True, () => "Between weeks should be available");
            Assert.That(actual: _evaluator.IsAvailable(unavailability, week2, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(10, 30, 0))), expression: Is.True, () => "Week 2 should be available (INTERVAL=2 skips)");
            Assert.That(actual: _evaluator.IsAvailable(unavailability, week3, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(10, 30, 0))), expression: Is.False, () => "Week 3 should be blocked (next occurrence)");
        });
    }

    [Test]
    public void DateBeforeStartDate_ShouldBeAvailable()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 8, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0)
        );

        var beforeDate = new DateOnly(year: 2025, month: 10, day: 19);

        Assert.That(actual: _evaluator.IsAvailable(unavailability, beforeDate, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(9, 30, 0))), expression: Is.True);
    }

    [Test]
    public void SlotCompletelyEncompassingUnavailability_ShouldBlock()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var date = DateOnly.FromDateTime(dateTime: unavailability.DateStart);
        var encompassingSlot = new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(13, 0, 0));

        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, encompassingSlot), expression: Is.False);
    }

    [Test]
    public void NoOverlapSlotBefore_ShouldBeAvailable()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var date = DateOnly.FromDateTime(dateTime: unavailability.DateStart);
        var beforeSlot = new TimeSlot(new TimeOnly(8, 0, 0), new TimeOnly(9, 59, 0));

        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, beforeSlot), expression: Is.True);
    }

    [Test]
    public void NoOverlapSlotAfter_ShouldBeAvailable()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 12, minute: 0, second: 0)
        );

        var date = DateOnly.FromDateTime(dateTime: unavailability.DateStart);
        var afterSlot = new TimeSlot(new TimeOnly(12, 1, 0), new TimeOnly(13, 0, 0));

        Assert.That(actual: _evaluator.IsAvailable(unavailability, date, afterSlot), expression: Is.True);
    }

    [Test]
    public void MonthlyLastDay_ShouldBlockOnLastDayOfMonth()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=MONTHLY;BYMONTHDAY=-1",
            DateStart: new DateTime(year: 2025, month: 10, day: 31, hour: 14, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 31, hour: 16, minute: 0, second: 0)
        );

        var lastDayOct = new DateOnly(year: 2025, month: 10, day: 31);
        var lastDayNov = new DateOnly(year: 2025, month: 11, day: 30);
        var otherDay = new DateOnly(year: 2025, month: 10, day: 30);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, lastDayOct, new TimeSlot(new TimeOnly(15, 0, 0), new TimeOnly(15, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, lastDayNov, new TimeSlot(new TimeOnly(15, 0, 0), new TimeOnly(15, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, otherDay, new TimeSlot(new TimeOnly(15, 0, 0), new TimeOnly(15, 30, 0))), expression: Is.True);
        });
    }

    [Test]
    public void WeeklyWithUntil_ShouldStopAfterUntil()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=WEEKLY;BYDAY=MO;UNTIL=20251101",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 9, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 11, minute: 0, second: 0)
        );

        var beforeUntil = new DateOnly(year: 2025, month: 10, day: 27); // Next Monday
        var afterUntil = new DateOnly(year: 2025, month: 11, day: 4); // Monday after Until

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, beforeUntil, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(10, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, afterUntil, new TimeSlot(new TimeOnly(10, 0, 0), new TimeOnly(10, 30, 0))), expression: Is.True);
        });
    }

    [Test]
    public void EventWithNullEndOnSingleDay_ShouldBlockFromStartToEndOfDay()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "", // Single event
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 13, minute: 0, second: 0),
            DateEnd: null
        );

        var date = new DateOnly(year: 2025, month: 10, day: 20);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(12, 0, 0), new TimeOnly(12, 30, 0))), expression: Is.True);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(13, 0, 0), new TimeOnly(14, 0, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, date, new TimeSlot(new TimeOnly(23, 0, 0), new TimeOnly(23, 30, 0))), expression: Is.False);
        });
    }

    [Test]
    public void ComplexRRule_MonthlyThirdWednesday_ShouldBlockCorrectly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=MONTHLY;BYDAY=WE;BYSETPOS=3",
            DateStart: new DateTime(year: 2025, month: 10, day: 15, hour: 10, minute: 0, second: 0), // Third Wednesday in Oct 2025
            DateEnd: new DateTime(year: 2025, month: 10, day: 15, hour: 12, minute: 0, second: 0)
        );

        var thirdWednesday = new DateOnly(year: 2025, month: 10, day: 15);
        var secondWednesday = new DateOnly(year: 2025, month: 10, day: 8);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, thirdWednesday, new TimeSlot(new TimeOnly(11, 0, 0), new TimeOnly(11, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, secondWednesday, new TimeSlot(new TimeOnly(11, 0, 0), new TimeOnly(11, 30, 0))), expression: Is.True);
        });
    }

    [Test]
    public void DailyWithInterval3_ShouldBlockEveryThirdDay()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            RRule: "FREQ=DAILY;INTERVAL=3",
            DateStart: new DateTime(year: 2025, month: 10, day: 20, hour: 8, minute: 0, second: 0),
            DateEnd: new DateTime(year: 2025, month: 10, day: 20, hour: 10, minute: 0, second: 0)
        );

        var day1 = new DateOnly(year: 2025, month: 10, day: 20);
        var day2 = new DateOnly(year: 2025, month: 10, day: 21);
        var day3 = new DateOnly(year: 2025, month: 10, day: 22);
        var day4 = new DateOnly(year: 2025, month: 10, day: 23);

        Assert.Multiple(testDelegate: () =>
        {
            Assert.That(actual: _evaluator.IsAvailable(unavailability, day1, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(9, 30, 0))), expression: Is.False);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, day2, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(9, 30, 0))), expression: Is.True);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, day3, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(9, 30, 0))), expression: Is.True);
            Assert.That(actual: _evaluator.IsAvailable(unavailability, day4, new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(9, 30, 0))), expression: Is.False);
        });
    }
}