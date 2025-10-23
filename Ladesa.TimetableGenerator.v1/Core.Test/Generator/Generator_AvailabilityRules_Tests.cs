using System.Globalization;
using Ladesa.TimetableGenerator.v1.Core.Domain;
using Ladesa.TimetableGenerator.v1.Core.Generator;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Ladesa.TimetableGenerator.Test;

[TestFixture]
public class Generator_AvailabilityRules_Tests
{
    [Test]
    public void Teacher_Unavailable_On_Specific_Day()
    {
        var date = new DateOnly(2025, 10, 27); // Monday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY;BYDAY=MO",
                date.ToDateTime(new TimeOnly(0, 0, 0)),
                date.ToDateTime(new TimeOnly(23, 59, 59))
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 1, 1);

        var request = new GenerateRequest(date, date, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(0), "Should generate 0 schedules due to teacher unavailability on Monday.");
    }

    [Test]
    public void Group_Unavailable_On_Specific_Time_Slot()
    {
        var date = new DateOnly(2025, 10, 27); // Monday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY",
                date.ToDateTime(new TimeOnly(8, 0, 0)),
                date.ToDateTime(new TimeOnly(8, 50, 0))
            )
        ]));
        var teacher = new Teacher("prof:1", new Availability([]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 1, 1);

        var request = new GenerateRequest(date, date, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(0), "Should generate 0 schedules due to group unavailability on specific time slot.");
    }

    [Test]
    public void Partial_Unavailability_Overlapping_Time_Slot()
    {
        var date = new DateOnly(2025, 10, 27); // Monday
        var timeSlot = new TimeSlot("08:00:00", "09:00:00");

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY",
                date.ToDateTime(new TimeOnly(8, 30, 0)),
                date.ToDateTime(new TimeOnly(9, 30, 0))
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 1, 1);

        var request = new GenerateRequest(date, date, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(0), "Should generate 0 schedules due to partial unavailability overlap.");
    }

    [Test]
    public void Unavailability_Recurring_Weekly()
    {
        var dateStart = new DateOnly(2025, 10, 27); // Monday
        var dateEnd = new DateOnly(2025, 10, 31); // Friday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=WEEKLY;BYDAY=MO,WE,FR",
                dateStart.ToDateTime(new TimeOnly(0, 0, 0)),
                dateStart.ToDateTime(new TimeOnly(23, 59, 59))
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 5, 5);

        var request = new GenerateRequest(dateStart, dateEnd, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(2), "Should generate schedules only on Tue and Thu.");

        var scheduledDates = result.Timetable.Schedules.Select(s => s.Date.DayOfWeek).ToList();
        Assert.That(scheduledDates, Has.No.Member(DayOfWeek.Monday));
        Assert.That(scheduledDates, Has.No.Member(DayOfWeek.Wednesday));
        Assert.That(scheduledDates, Has.No.Member(DayOfWeek.Friday));
        Assert.That(scheduledDates, Has.Member(DayOfWeek.Tuesday));
        Assert.That(scheduledDates, Has.Member(DayOfWeek.Thursday));
    }

    [Test]
    public void Unavailability_For_Specific_Date_Range()
    {
        var dateStart = new DateOnly(2025, 10, 27); // Monday
        var dateEnd = new DateOnly(2025, 10, 31); // Friday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var unavailableStart = dateStart.AddDays(2).ToDateTime(new TimeOnly(0, 0, 0)); // Wednesday
        var unavailableEnd = dateEnd.ToDateTime(new TimeOnly(23, 59, 59)); // Friday

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY",
                unavailableStart,
                unavailableEnd
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 5, 5);

        var request = new GenerateRequest(dateStart, dateEnd, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(2), "Should generate schedules only outside unavailability (Mon, Tue).");

        var scheduledDates = result.Timetable.Schedules.Select(s => s.Date).ToList();
        Assert.That(scheduledDates, Does.Contain(dateStart)); // Mon
        Assert.That(scheduledDates, Does.Contain(dateStart.AddDays(1))); // Tue
        Assert.That(scheduledDates, Does.Not.Contain(dateStart.AddDays(2))); // Wed
        Assert.That(scheduledDates, Does.Not.Contain(dateStart.AddDays(3))); // Thu
        Assert.That(scheduledDates, Does.Not.Contain(dateStart.AddDays(4))); // Fri
    }

    [Test]
    public void Empty_Availability_Rules()
    {
        var date = new DateOnly(2025, 10, 27); // Monday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 1, 1);

        var request = new GenerateRequest(date, date, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(1), "Should generate schedule with full availability.");
    }

    [Test]
    public void Group_And_Teacher_Both_Unavailable()
    {
        var date = new DateOnly(2025, 10, 27); // Monday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY",
                date.ToDateTime(new TimeOnly(0, 0, 0)),
                date.ToDateTime(new TimeOnly(23, 59, 59))
            )
        ]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY",
                date.ToDateTime(new TimeOnly(0, 0, 0)),
                date.ToDateTime(new TimeOnly(23, 59, 59))
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 1, 1);

        var request = new GenerateRequest(date, date, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(0), "Should generate 0 schedules when both are unavailable.");
    }

    [Test]
    public void Unavailability_Overlapping_Partial_Week()
    {
        var dateStart = new DateOnly(2025, 10, 27); // Monday
        var dateEnd = new DateOnly(2025, 10, 31); // Friday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var unavailableDate = new DateOnly(2025, 11, 1); // Saturday, outside range
        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY",
                unavailableDate.ToDateTime(new TimeOnly(0, 0, 0)),
                unavailableDate.ToDateTime(new TimeOnly(23, 59, 59))
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 5, 5);

        var request = new GenerateRequest(dateStart, dateEnd, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(5), "Should generate full schedules with no impact from weekend unavailability.");
    }

    [Test]
    public void Multiple_Unavailability_Rules_Per_Entity()
    {
        var date = new DateOnly(2025, 10, 27); // Monday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability( // Day-specific
                "FREQ=DAILY;BYDAY=MO",
                date.ToDateTime(new TimeOnly(0, 0, 0)),
                date.ToDateTime(new TimeOnly(23, 59, 59))
            ),
            new AvailabilityRuleUnavailability( // Time-specific
                "FREQ=DAILY",
                date.ToDateTime(new TimeOnly(8, 0, 0)),
                date.ToDateTime(new TimeOnly(9, 0, 0))
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 1, 1);

        var request = new GenerateRequest(date, date, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(0), "Should generate 0 schedules due to multiple unavailability rules.");
    }

    [Test]
    public void RRULE_With_Interval_Every_Other_Week()
    {
        var dateStart = new DateOnly(2025, 10, 27); // Monday, week 1
        var dateMid = new DateOnly(2025, 11, 3); // Next Monday, week 2
        var dateEnd = new DateOnly(2025, 11, 10); // Following Monday, week 3
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=WEEKLY;INTERVAL=2;BYDAY=MO",
                dateStart.ToDateTime(new TimeOnly(0, 0, 0)),
                dateStart.ToDateTime(new TimeOnly(23, 59, 59))
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 3, 3);

        var request = new GenerateRequest(dateStart, dateEnd, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(1), "Should generate schedule only on the available Monday (every other week).");

        var scheduledDate = result.Timetable.Schedules[0].Date;
        Assert.That(scheduledDate.DayOfWeek, Is.EqualTo(DayOfWeek.Monday));
        Assert.That(scheduledDate, Is.EqualTo(dateMid), "Should be available on even weeks, unavailable on odd.");
    }

    [Test]
    public void Unavailability_With_Exact_Time_Match()
    {
        var date = new DateOnly(2025, 10, 27); // Monday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY",
                date.ToDateTime(new TimeOnly(8, 0, 0)),
                date.ToDateTime(new TimeOnly(8, 50, 0))
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 1, 1);

        var request = new GenerateRequest(date, date, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(0), "Should generate 0 schedules due to exact time match unavailability.");
    }

    [Test]
    public void RRULE_With_Count_Limit()
    {
        var dateStart = new DateOnly(2025, 10, 27); // Monday 1
        var dateEnd = new DateOnly(2025, 11, 17); // Monday 4 (4 weeks later)
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=WEEKLY;COUNT=3;BYDAY=MO",
                dateStart.ToDateTime(new TimeOnly(0, 0, 0)),
                dateStart.ToDateTime(new TimeOnly(23, 59, 59))
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 4, 4);

        var request = new GenerateRequest(dateStart, dateEnd, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(1), "Should generate schedule only on the 4th Monday (after count=3 unavailabilities).");

        var scheduledDates = result.Timetable.Schedules.Select(s => s.Date).ToList();
        Assert.That(scheduledDates, Does.Not.Contain(dateStart)); // Unavailable 1
        Assert.That(scheduledDates, Does.Not.Contain(dateStart.AddDays(7))); // Unavailable 2
        Assert.That(scheduledDates, Does.Not.Contain(dateStart.AddDays(14))); // Unavailable 3
        Assert.That(scheduledDates, Does.Contain(dateStart.AddDays(21))); // Available 4
    }

    [Test]
    public void Unavailability_DateStart_After_Request_DateEnd()
    {
        var dateStart = new DateOnly(2025, 10, 27); // Monday
        var dateEnd = new DateOnly(2025, 10, 31); // Friday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var unavailableStart = dateEnd.AddDays(1).ToDateTime(new TimeOnly(0, 0, 0)); // Saturday after range

        var group = new Group("turma:1", new Availability([]));
        var teacher = new Teacher("prof:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY",
                unavailableStart,
                unavailableStart.AddDays(1)
            )
        ]));
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 5, 5);

        var request = new GenerateRequest(dateStart, dateEnd, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(5), "Should generate full schedules with no impact from unavailability after range.");
    }

    [Test]
    public void Group_Unavailability_Overriding_Teacher_Availability()
    {
        var date = new DateOnly(2025, 10, 27); // Monday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var group = new Group("turma:1", new Availability([
            new AvailabilityRuleUnavailability(
                "FREQ=DAILY",
                date.ToDateTime(new TimeOnly(0, 0, 0)),
                date.ToDateTime(new TimeOnly(23, 59, 59))
            )
        ]));
        var teacher = new Teacher("prof:1", new Availability([])); // Teacher available
        var diary = new Diary("diario:1", group.Id, teacher.Id, "disc:1", 1, 1);

        var request = new GenerateRequest(date, date, [group], [teacher], [diary], [timeSlot]);

        var result = Generator.GenerateTimetables(request).FirstOrDefault();

        Assert.That(result, Is.Not.Null, "Should generate at least one timetable.");
        Assert.That(result!.Timetable.Schedules, Has.Length.EqualTo(0), "Should generate 0 schedules due to group unavailability overriding teacher availability.");
    }
}