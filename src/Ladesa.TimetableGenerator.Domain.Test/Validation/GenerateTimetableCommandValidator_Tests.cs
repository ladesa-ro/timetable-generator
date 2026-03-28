using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Exceptions;
using Ladesa.TimetableGenerator.Domain.Test.TestUtilities;

namespace Ladesa.TimetableGenerator.Domain.Test.Validation;

[TestFixture]
public class GenerateTimetableCommandValidator_Tests
{
    [Test]
    public void Invalid_TimeSlot_StartEqualsEnd_ShouldThrow()
    {
        var date = new DateOnly(2025, 10, 27);
        var slot = Builders.Slot("08:00:00", "08:00:00");

        var command = Builders.Request(
            start: date,
            end: date,
            groups: [Builders.Group()],
            teachers: [Builders.Teacher()],
            diaries: [Builders.Diary("d1", "group:1", "teacher:1")],
            timeSlots: [slot]
        );

        Assert.Throws<ArgumentException>(() => GenerateTimetableCommandValidator.Validate(command));
    }

    [Test]
    public void Invalid_TimeSlot_EndBeforeStart_ShouldThrow()
    {
        var date = new DateOnly(2025, 10, 27);
        var slot = Builders.Slot("09:00:00", "08:00:00");

        var command = Builders.Request(
            start: date,
            end: date,
            groups: [Builders.Group()],
            teachers: [Builders.Teacher()],
            diaries: [Builders.Diary("d1", "group:1", "teacher:1")],
            timeSlots: [slot]
        );

        Assert.Throws<ArgumentException>(() => GenerateTimetableCommandValidator.Validate(command));
    }

    [Test]
    public void Duplicate_Group_IDs_ShouldThrow()
    {
        var date = new DateOnly(2025, 10, 27);
        var g1 = Builders.Group("group:1");
        var g2 = Builders.Group("group:1");

        var command = Builders.Request(
            start: date,
            end: date,
            groups: [g1, g2],
            teachers: [Builders.Teacher()],
            diaries: [Builders.Diary("d1", g1.Id, "teacher:1")],
            timeSlots: [Builders.Slot("08:00:00", "08:50:00")]
        );

        Assert.Throws<GeneratorValidationException>(() => GenerateTimetableCommandValidator.Validate(command));
    }

    [Test]
    public void Duplicate_Teacher_IDs_ShouldThrow()
    {
        var date = new DateOnly(2025, 10, 27);
        var t1 = Builders.Teacher("teacher:1");
        var t2 = Builders.Teacher("teacher:1");

        var command = Builders.Request(
            start: date,
            end: date,
            groups: [Builders.Group()],
            teachers: [t1, t2],
            diaries: [Builders.Diary("d1", "group:1", t1.Id)],
            timeSlots: [Builders.Slot("08:00:00", "08:50:00")]
        );

        Assert.Throws<GeneratorValidationException>(() => GenerateTimetableCommandValidator.Validate(command));
    }

    [Test]
    public void Duplicate_Diary_IDs_ShouldThrow()
    {
        var date = new DateOnly(2025, 10, 27);
        var g = Builders.Group();
        var t = Builders.Teacher();
        var d1 = Builders.Diary("diary:1", g.Id, t.Id);
        var d2 = Builders.Diary("diary:1", g.Id, t.Id);

        var command = Builders.Request(
            start: date,
            end: date,
            groups: [g],
            teachers: [t],
            diaries: [d1, d2],
            timeSlots: [Builders.Slot("08:00:00", "08:50:00")]
        );

        Assert.Throws<GeneratorValidationException>(() => GenerateTimetableCommandValidator.Validate(command));
    }

    [Test]
    public void DiaryReferencesMissingGroup_ShouldThrow()
    {
        var date = new DateOnly(2025, 10, 27);
        var g = Builders.Group("group:1");
        var t = Builders.Teacher("teacher:1");
        var d = Builders.Diary("diary:1", groupId: "group:missing", teacherId: t.Id);
        var req = Builders.Request(date, date, [g], [t], [d], [Builders.Slot("08:00:00", "08:50:00")]);

        var ex = Assert.Throws<GeneratorValidationException>(() => GenerateTimetableCommandValidator.Validate(req));
        Assert.That(ex!.Message, Does.Contain("Group not found"));
    }

    [Test]
    public void DiaryReferencesMissingTeacher_ShouldThrow()
    {
        var date = new DateOnly(2025, 10, 27);
        var g = Builders.Group("group:1");
        var t = Builders.Teacher("teacher:1");
        var d = Builders.Diary("diary:1", groupId: g.Id, teacherId: "teacher:missing");
        var req = Builders.Request(date, date, [g], [t], [d], [Builders.Slot("08:00:00", "08:50:00")]);

        var ex = Assert.Throws<GeneratorValidationException>(() => GenerateTimetableCommandValidator.Validate(req));
        Assert.That(ex!.Message, Does.Contain("Teacher not found"));
    }

    [Test]
    public void DiaryReferencesMissingBoth_ShouldThrow()
    {
        var date = new DateOnly(2025, 10, 27);
        var g = Builders.Group("group:1");
        var t = Builders.Teacher("teacher:1");
        var d = Builders.Diary("diary:1", groupId: "group:missing", teacherId: "teacher:missing");
        var req = Builders.Request(date, date, [g], [t], [d], [Builders.Slot("08:00:00", "08:50:00")]);

        var ex = Assert.Throws<GeneratorValidationException>(() => GenerateTimetableCommandValidator.Validate(req));
        Assert.That(ex!.Message, Does.Contain("Diary references not found"));
    }
}
