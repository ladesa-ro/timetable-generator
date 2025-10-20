using System.Text.Json;
using Ladesa.TimetableGenerator.v1.Core.Domain;
using Ladesa.TimetableGenerator.v1.Core.Generator;


namespace Ladesa.TimetableGenerator.Core.Test;

[TestFixture]
public class SimpleGeradorTests
{
    private static GenerateRequest BuildBasicPayload(
        DateOnly date,
        TimeSlot[] timeSlots,
        Availability? groupAvailability = null,
        Availability? teacherAvailability = null,
        int diaryWeekLimit = 1
    )
    {
        var group = new Group(
            "turma:1",
            groupAvailability ?? new Availability([])
        );

        var teacher = new Teacher(
            "prof:1",
            teacherAvailability ?? new Availability([])
        );

        var diary = new Diary(
            "diario:1",
            group.Id,
            teacher.Id,
            "disc:1",
            diaryWeekLimit,
            100
        );

        return new GenerateRequest(
            date,
            date,
            [group],
            [teacher],
            [diary],
            timeSlots
        );
    }

    [Test]
    public void GerarHorario_SimpleCase_ReturnsOneAula()
    {
        var date = new DateOnly(2025, 1, 6); // Monday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        var payload = BuildBasicPayload(date, [timeSlot]);

        var generatedTimetables = Generator.GenerateTimetables(payload);
        var generatedTimetable = generatedTimetables.FirstOrDefault();

        Assert.That(generatedTimetable, Is.Not.Null, "Deveria gerar ao menos um horário");

        Assert.That(
            generatedTimetable!.Timetable.Schedules,
            Has.Length.EqualTo(1),
            "Deveria agendar 1 aula no cenário básico"
        );

        var timetableSchedule = generatedTimetable.Timetable.Schedules[0];
        using (Assert.EnterMultipleScope())
        {
            Assert.That(timetableSchedule.GroupId, Is.EqualTo("turma:1"));
            Assert.That(timetableSchedule.DiaryId, Is.EqualTo("diario:1"));
            Assert.That(timetableSchedule.TeacherId, Is.EqualTo("prof:1"));
            Assert.That(timetableSchedule.Date, Is.EqualTo(date));
            Assert.That(timetableSchedule.TimeSlot, Is.EqualTo(timeSlot));
        }
    }

    [Test]
    public void GerarHorario_IndisponibilidadeDiaDaSemana_CurrentBehavior_AllowsAula()
    {
        var date = new DateOnly(2025, 1, 6); // Monday
        var timeSlot = new TimeSlot("08:00:00", "08:50:00");

        // Professor com regra de "indisponibilidade" na segunda o dia todo.
        // O comportamento atual do avaliador considera true quando o slot está dentro da janela configurada.
        var payload = BuildBasicPayload(
            date,
            [timeSlot],
            new Availability([]),
            new Availability(
                [
                    new AvailabilityRuleUnavailability(
                        RRule: "FREQ=DAILY;BYDAY=MO",
                        DateStart: date.ToDateTime(TimeOnly.Parse("00:00:00")), 
                        DateEnd: date.ToDateTime(TimeOnly.Parse("23:59:00"))
                    )
                ]
            ),
            1
        );

        var generatedTimetables = Generator.GenerateTimetables(payload);
        var generatedTimetable = generatedTimetables.FirstOrDefault();

        Console.WriteLine(JsonSerializer.Serialize(
            generatedTimetable,
            new JsonSerializerOptions
            {
                WriteIndented = true
            }));


        Assert.That(generatedTimetable, Is.Not.Null, "Deveria gerar um horário");
        Assert.That(
            generatedTimetable!.Timetable.Schedules,
            Has.Length.EqualTo(0),
            "Nenhuma aula gerada"
        );
    }
}