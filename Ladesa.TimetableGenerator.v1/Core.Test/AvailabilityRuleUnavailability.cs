using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ladesa.TimetableGenerator.v1.Core.Domain;

namespace Ladesa.TimetableGenerator.Test;

[TestFixture]
public class AvailabilityRuleUnavailabilityDetailedTests
{
    [Test]
    public void AllDayEveryDay_ShouldBlockAllDay()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 0, 0, 0),
            new DateTime(2025, 10, 20, 23, 59, 59)
        );

        var date = DateOnly.FromDateTime(unavailability.DateStart);

        // Dentro do período
        var slotInside = new TimeSlot("08:00", "10:00");
        Assert.That(unavailability.IsAvailable(date, slotInside), Is.False);

        // Antes do período
        var slotBefore = new TimeSlot("00:00", "00:30");
        Assert.That(unavailability.IsAvailable(date, slotBefore), Is.False);

        // Depois do período
        var slotAfter = new TimeSlot("23:00", "23:59");
        Assert.That(unavailability.IsAvailable(date, slotAfter), Is.False);
    }

    [Test]
    public void EveryMorning_ShouldBlockMorningOnly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 8, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var date = DateOnly.FromDateTime(unavailability.DateStart);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("08:30", "09:30")), Is.False);
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("07:00", "07:50")), Is.True);
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("12:30", "13:30")), Is.True);
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("11:50", "12:10")), Is.False);
        });
    }

    [Test]
    public void EveryWednesdayAfternoon_ShouldBlockOnlyOnWednesday()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=WEEKLY;BYDAY=WE",
            new DateTime(2025, 10, 22, 14, 0, 0),
            new DateTime(2025, 10, 22, 18, 0, 0)
        );

        var wednesday = new DateOnly(2025, 10, 22); // Wednesday
        var thursday = new DateOnly(2025, 10, 23); // Thursday

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(wednesday, new TimeSlot("15:00", "16:00")), Is.False);
            Assert.That(unavailability.IsAvailable(thursday, new TimeSlot("15:00", "16:00")), Is.True);
        });
    }

    [Test]
    public void EventWithoutEndDate_ShouldBlockFromTimeOnwardsForever()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 10, 0, 0),
            null
        );

        var date = new DateOnly(2025, 10, 21);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("10:00", "11:00")), Is.False);
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("12:00", "13:00")), Is.False);
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("09:00", "09:30")), Is.True);
        });
    }

    [Test]
    public void MonthlyEvent_OnFirstDay_ShouldBlockCorrectly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=MONTHLY;BYMONTHDAY=1",
            new DateTime(2025, 11, 1, 8, 0, 0),
            new DateTime(2025, 11, 1, 12, 0, 0)
        );

        var firstOfMonth = new DateOnly(2025, 11, 1);
        var secondOfMonth = new DateOnly(2025, 11, 2);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(firstOfMonth, new TimeSlot("09:00", "10:00")), Is.False);
            Assert.That(unavailability.IsAvailable(secondOfMonth, new TimeSlot("09:00", "10:00")), Is.True);
        });
    }

    [Test]
    public void LastFridayOfMonth_ShouldBlockCorrectFridayOnly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=MONTHLY;BYDAY=FR;BYSETPOS=-1",
            new DateTime(2025, 10, 31, 15, 0, 0),
            new DateTime(2025, 10, 31, 18, 0, 0)
        );

        var lastFriday = new DateOnly(2025, 10, 31);
        var otherFriday = new DateOnly(2025, 10, 24);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(lastFriday, new TimeSlot("16:00", "17:00")), Is.False);
            Assert.That(unavailability.IsAvailable(otherFriday, new TimeSlot("16:00", "17:00")), Is.True);
        });
    }

    [Test]
    public void EveryOtherDay_ShouldBlockEveryOtherDay()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY;INTERVAL=2",
            new DateTime(2025, 10, 20, 9, 0, 0),
            new DateTime(2025, 10, 20, 17, 0, 0)
        );

        var firstDay = new DateOnly(2025, 10, 20);
        var secondDay = new DateOnly(2025, 10, 21);
        var thirdDay = new DateOnly(2025, 10, 22);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(firstDay, new TimeSlot("10:00", "11:00")), Is.False);
            Assert.That(unavailability.IsAvailable(secondDay, new TimeSlot("10:00", "11:00")), Is.True);
            Assert.That(unavailability.IsAvailable(thirdDay, new TimeSlot("10:00", "11:00")), Is.False);
        });
    }

    // Additional 20 test cases below

    [Test]
    public void SingleEvent_ShouldBlockOnlyOnStartDate()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "", // Assuming empty RRULE means single event
            new DateTime(2025, 10, 20, 9, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var startDate = new DateOnly(2025, 10, 20);
        var nextDate = new DateOnly(2025, 10, 21);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(startDate, new TimeSlot("10:00", "11:00")), Is.False);
            Assert.That(unavailability.IsAvailable(nextDate, new TimeSlot("10:00", "11:00")), Is.True);
        });
    }

    [Test]
    public void OverlappingSlot_StartsBeforeEndsDuring_ShouldBlock()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 10, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var date = DateOnly.FromDateTime(unavailability.DateStart);
        var overlappingSlot = new TimeSlot("09:30", "10:30");

        Assert.That(unavailability.IsAvailable(date, overlappingSlot), Is.False);
    }

    [Test]
    public void OverlappingSlot_StartsDuringEndsAfter_ShouldBlock()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 10, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var date = DateOnly.FromDateTime(unavailability.DateStart);
        var overlappingSlot = new TimeSlot("11:30", "12:30");

        Assert.That(unavailability.IsAvailable(date, overlappingSlot), Is.False);
    }

    [Test]
    public void SlotTouchingStart_EndsAtStart_ShouldBeAvailable() // Assuming no overlap if ends exactly at start
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 10, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var date = DateOnly.FromDateTime(unavailability.DateStart);
        var touchingSlot = new TimeSlot("09:00", "10:00");

        Assert.That(unavailability.IsAvailable(date, touchingSlot), Is.True);
    }

    [Test]
    public void SlotTouchingEnd_StartsAtEnd_ShouldBeAvailable() // Assuming no overlap if starts exactly at end
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 10, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var date = DateOnly.FromDateTime(unavailability.DateStart);
        var touchingSlot = new TimeSlot("12:00", "13:00");

        Assert.That(unavailability.IsAvailable(date, touchingSlot), Is.True);
    }

    [Test]
    public void WeeklyWithMultipleDays_ShouldBlockOnSpecifiedDays()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=WEEKLY;BYDAY=MO,WE,FR",
            new DateTime(2025, 10, 20, 13, 0, 0), // Monday
            new DateTime(2025, 10, 20, 15, 0, 0)
        );

        var monday = new DateOnly(2025, 10, 20);
        var tuesday = new DateOnly(2025, 10, 21);
        var wednesday = new DateOnly(2025, 10, 22);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(monday, new TimeSlot("14:00", "14:30")), Is.False);
            Assert.That(unavailability.IsAvailable(tuesday, new TimeSlot("14:00", "14:30")), Is.True);
            Assert.That(unavailability.IsAvailable(wednesday, new TimeSlot("14:00", "14:30")), Is.False);
        });
    }

    [Test]
    public void MonthlyByDayOfWeek_SecondTuesday_ShouldBlockCorrectly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=MONTHLY;BYDAY=TU;BYSETPOS=2",
            new DateTime(2025, 10, 14, 9, 0, 0), // Second Tuesday in Oct 2025
            new DateTime(2025, 10, 14, 11, 0, 0)
        );

        var secondTuesday = new DateOnly(2025, 10, 14);
        var firstTuesday = new DateOnly(2025, 10, 7);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(secondTuesday, new TimeSlot("10:00", "10:30")), Is.False);
            Assert.That(unavailability.IsAvailable(firstTuesday, new TimeSlot("10:00", "10:30")), Is.True);
        });
    }

    [Test]
    public void YearlyEvent_ShouldBlockOnAnniversary()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=YEARLY",
            new DateTime(2025, 10, 20, 8, 0, 0),
            new DateTime(2025, 10, 20, 17, 0, 0)
        );

        var sameDayThisYear = new DateOnly(2025, 10, 20);
        var sameDayNextYear = new DateOnly(2026, 10, 20);
        var differentDay = new DateOnly(2025, 10, 21);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(sameDayThisYear, new TimeSlot("09:00", "10:00")), Is.False);
            Assert.That(unavailability.IsAvailable(sameDayNextYear, new TimeSlot("09:00", "10:00")), Is.False);
            Assert.That(unavailability.IsAvailable(differentDay, new TimeSlot("09:00", "10:00")), Is.True);
        });
    }

    [Test]
    public void WithUntilDate_ShouldBlockOnlyBeforeUntil()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY;UNTIL=20251025T120000",
            new DateTime(2025, 10, 20, 10, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var beforeUntil = new DateOnly(2025, 10, 24);
        var onUntil = new DateOnly(2025, 10, 25);
        var afterUntil = new DateOnly(2025, 10, 26);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(beforeUntil, new TimeSlot("11:00", "11:30")), Is.False);
            Assert.That(unavailability.IsAvailable(onUntil, new TimeSlot("11:00", "11:30")), Is.False);
            Assert.That(unavailability.IsAvailable(afterUntil, new TimeSlot("11:00", "11:30")), Is.True);
        });
    }

    [Test]
    public void WithCount_ShouldBlockOnlyForSpecifiedOccurrences()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY;COUNT=3",
            new DateTime(2025, 10, 20, 14, 0, 0),
            new DateTime(2025, 10, 20, 16, 0, 0)
        );

        var firstDay = new DateOnly(2025, 10, 20);
        var secondDay = new DateOnly(2025, 10, 21);
        var thirdDay = new DateOnly(2025, 10, 22);
        var fourthDay = new DateOnly(2025, 10, 23);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(firstDay, new TimeSlot("15:00", "15:30")), Is.False);
            Assert.That(unavailability.IsAvailable(secondDay, new TimeSlot("15:00", "15:30")), Is.False);
            Assert.That(unavailability.IsAvailable(thirdDay, new TimeSlot("15:00", "15:30")), Is.False);
            Assert.That(unavailability.IsAvailable(fourthDay, new TimeSlot("15:00", "15:30")), Is.True);
        });
    }

    [Test]
    public void EveryTwoWeeks_ShouldBlockAccordingly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=WEEKLY;INTERVAL=2",
            new DateTime(2025, 10, 20, 9, 0, 0), // Monday
            new DateTime(2025, 10, 20, 11, 0, 0)
        );


        var calendarEvent = new CalendarEvent
        {
            DtStart = new CalDateTime(unavailability.DateStart),
            DtEnd = new CalDateTime((DateTime)unavailability.DateEnd),
            RecurrenceRules = [unavailability.RecurrencePattern]
        };


        var week1 = new DateOnly(2025, 10, 20);
        var week2 = new DateOnly(2025, 10, 27);
        var between = new DateOnly(2025, 10, 24); // Friday of first week, but since no BYDAY, assumes same day

        var r = calendarEvent.GetOccurrences(new CalDateTime(week2)).Take(2).ToArray();

        Console.WriteLine(r);


        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(week1, new TimeSlot("10:00", "10:30")), Is.False);
            Assert.That(unavailability.IsAvailable(between, new TimeSlot("10:00", "10:30")), Is.True);
            Assert.That(unavailability.IsAvailable(week2, new TimeSlot("10:00", "10:30")), Is.False);
        });
    }

    [Test]
    public void DateBeforeStartDate_ShouldBeAvailable()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 8, 0, 0),
            new DateTime(2025, 10, 20, 10, 0, 0)
        );

        var beforeDate = new DateOnly(2025, 10, 19);

        Assert.That(unavailability.IsAvailable(beforeDate, new TimeSlot("09:00", "09:30")), Is.True);
    }

    [Test]
    public void SlotCompletelyEncompassingUnavailability_ShouldBlock()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 10, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var date = DateOnly.FromDateTime(unavailability.DateStart);
        var encompassingSlot = new TimeSlot("09:00", "13:00");

        Assert.That(unavailability.IsAvailable(date, encompassingSlot), Is.False);
    }

    [Test]
    public void NoOverlapSlotBefore_ShouldBeAvailable()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 10, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var date = DateOnly.FromDateTime(unavailability.DateStart);
        var beforeSlot = new TimeSlot("08:00", "09:59");

        Assert.That(unavailability.IsAvailable(date, beforeSlot), Is.True);
    }

    [Test]
    public void NoOverlapSlotAfter_ShouldBeAvailable()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY",
            new DateTime(2025, 10, 20, 10, 0, 0),
            new DateTime(2025, 10, 20, 12, 0, 0)
        );

        var date = DateOnly.FromDateTime(unavailability.DateStart);
        var afterSlot = new TimeSlot("12:01", "13:00");

        Assert.That(unavailability.IsAvailable(date, afterSlot), Is.True);
    }

    [Test]
    public void MonthlyLastDay_ShouldBlockOnLastDayOfMonth()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=MONTHLY;BYMONTHDAY=-1",
            new DateTime(2025, 10, 31, 14, 0, 0),
            new DateTime(2025, 10, 31, 16, 0, 0)
        );

        var lastDayOct = new DateOnly(2025, 10, 31);
        var lastDayNov = new DateOnly(2025, 11, 30);
        var otherDay = new DateOnly(2025, 10, 30);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(lastDayOct, new TimeSlot("15:00", "15:30")), Is.False);
            Assert.That(unavailability.IsAvailable(lastDayNov, new TimeSlot("15:00", "15:30")), Is.False);
            Assert.That(unavailability.IsAvailable(otherDay, new TimeSlot("15:00", "15:30")), Is.True);
        });
    }

    [Test]
    public void WeeklyWithUntil_ShouldStopAfterUntil()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=WEEKLY;BYDAY=MO;UNTIL=20251101",
            new DateTime(2025, 10, 20, 9, 0, 0),
            new DateTime(2025, 10, 20, 11, 0, 0)
        );

        var beforeUntil = new DateOnly(2025, 10, 27); // Next Monday
        var afterUntil = new DateOnly(2025, 11, 4); // Monday after Until

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(beforeUntil, new TimeSlot("10:00", "10:30")), Is.False);
            Assert.That(unavailability.IsAvailable(afterUntil, new TimeSlot("10:00", "10:30")), Is.True);
        });
    }

    [Test]
    public void EventWithNullEndOnSingleDay_ShouldBlockFromStartToEndOfDay()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "", // Single event
            new DateTime(2025, 10, 20, 13, 0, 0),
            null
        );

        var date = new DateOnly(2025, 10, 20);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("12:00", "12:30")), Is.True);
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("13:00", "14:00")), Is.False);
            Assert.That(unavailability.IsAvailable(date, new TimeSlot("23:00", "23:30")), Is.False);
        });
    }

    [Test]
    public void ComplexRRule_MonthlyThirdWednesday_ShouldBlockCorrectly()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=MONTHLY;BYDAY=WE;BYSETPOS=3",
            new DateTime(2025, 10, 15, 10, 0, 0), // Third Wednesday in Oct 2025
            new DateTime(2025, 10, 15, 12, 0, 0)
        );

        var thirdWednesday = new DateOnly(2025, 10, 15);
        var secondWednesday = new DateOnly(2025, 10, 8);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(thirdWednesday, new TimeSlot("11:00", "11:30")), Is.False);
            Assert.That(unavailability.IsAvailable(secondWednesday, new TimeSlot("11:00", "11:30")), Is.True);
        });
    }

    [Test]
    public void DailyWithInterval3_ShouldBlockEveryThirdDay()
    {
        var unavailability = new AvailabilityRuleUnavailability(
            "FREQ=DAILY;INTERVAL=3",
            new DateTime(2025, 10, 20, 8, 0, 0),
            new DateTime(2025, 10, 20, 10, 0, 0)
        );

        var day1 = new DateOnly(2025, 10, 20);
        var day2 = new DateOnly(2025, 10, 21);
        var day3 = new DateOnly(2025, 10, 22);
        var day4 = new DateOnly(2025, 10, 23);

        Assert.Multiple(() =>
        {
            Assert.That(unavailability.IsAvailable(day1, new TimeSlot("09:00", "09:30")), Is.False);
            Assert.That(unavailability.IsAvailable(day2, new TimeSlot("09:00", "09:30")), Is.True);
            Assert.That(unavailability.IsAvailable(day3, new TimeSlot("09:00", "09:30")), Is.True);
            Assert.That(unavailability.IsAvailable(day4, new TimeSlot("09:00", "09:30")), Is.False);
        });
    }
}