using System.Text.Json;
using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;
using Ladesa.TimetableGenerator.Features.Gerador;

namespace Ladesa.TimetableGenerator.Test;

[TestFixture]
public class SimpleGeradorTests
{
    private static GeneratorPayload BuildBasicPayload(
        DateOnly data,
        TimeSlot[] slots,
        AvailabilityRule? turmaDisponibilidade = null,
        AvailabilityRule? profDisponibilidade = null,
        int maxAulasSemana = 1
    )
    {
        var turma = new Group(
            "turma:1",
            turmaDisponibilidade ?? new AvailabilityRuleCompound([])
        );

        var professor = new Teacher(
            "prof:1",
            profDisponibilidade ?? new AvailabilityRuleCompound([])
        );

        var diario = new Diary(
            "diario:1",
            turma.Id,
            professor.Id,
            "disc:1",
            maxAulasSemana,
            100
        );

        return new GeneratorPayload(
            Guid.NewGuid(),
            data,
            data,
            new[] { turma },
            new[] { professor },
            new[] { diario },
            slots
        );
    }

    [Test]
    public void GerarHorario_SimpleCase_ReturnsOneAula()
    {
        var data = new DateOnly(2025, 1, 6); // Monday
        var slot = new TimeSlot("08:00:00", "08:50:00");
        var payload = BuildBasicPayload(data, new[] { slot });

        var horarios = Gerador.GerarHorario(payload);
        var primeiro = horarios.FirstOrDefault();

        Assert.That(primeiro, Is.Not.Null, "Deveria gerar ao menos um horário");
        Assert.That(
            primeiro!.Schedules.Length,
            Is.EqualTo(1),
            "Deveria agendar 1 aula no cenário básico"
        );

        var aula = primeiro.Schedules[0];
        Assert.Multiple(() =>
        {
            Assert.That(aula.GroupId, Is.EqualTo("turma:1"));
            Assert.That(aula.DiaryId, Is.EqualTo("diario:1"));
            Assert.That(aula.TeacherId, Is.EqualTo("prof:1"));
            Assert.That(aula.Date, Is.EqualTo(data));
            Assert.That(aula.TimeSlot, Is.EqualTo(slot));
        });
    }

    [Test]
    public void GerarHorario_IndisponibilidadeDiaDaSemana_CurrentBehavior_AllowsAula()
    {
        var data = new DateOnly(2025, 1, 6); // Monday
        var slot = new TimeSlot("08:00:00", "08:50:00");

        var indisponibilidadeDiaTodo = new AvailabilityRuleCompound(
            new AvailabilityRule[]
            {
                new AvailabilityRuleUnavailableWeekDay(
                    DayOfWeek.Monday,
                    new TimeSlot("00:00:00", "23:59:59")
                )
            }
        );

        // Professor com regra de "indisponibilidade" na segunda o dia todo.
        // O comportamento atual do avaliador considera true quando o slot está dentro da janela configurada.
        var payload = BuildBasicPayload(
            data,
            new[] { slot },
            new AvailabilityRuleCompound(Array.Empty<AvailabilityRule>()),
            indisponibilidadeDiaTodo,
            1
        );

        var horarios = Gerador.GerarHorario(payload);
        var primeiro = horarios.FirstOrDefault();

        Assert.That(primeiro, Is.Not.Null, "Deveria gerar um horário");
        Assert.That(
            primeiro!.Schedules.Length,
            Is.EqualTo(1),
            "Comportamento atual permite agendamento dentro da janela marcada como 'indisponível'"
        );
    }
}