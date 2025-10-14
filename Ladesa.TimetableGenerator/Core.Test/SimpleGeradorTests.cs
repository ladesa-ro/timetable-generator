using System.Linq;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation;
using Ladesa.TimetableGenerator.Features.Gerador;

namespace Ladesa.TimetableGenerator.Test;

[TestFixture]
public class SimpleGeradorTests
{
    private static GeradorPayload BuildBasicPayload(
        DateOnly data,
        SlotDeTempo[] slots,
        RegraDisponibilidade? turmaDispon = null,
        RegraDisponibilidade? profDispon = null,
        int maxAulasSemana = 1
    )
    {
        var turma = new Turma(
            Id: "turma:1",
            RegraDisponibilidade: turmaDispon ?? new RegraDisponibilidade(Array.Empty<IRegraDisponibilidade>())
        );

        var professor = new Professor(
            Id: "prof:1",
            RegraDisponibilidade: profDispon ?? new RegraDisponibilidade(Array.Empty<IRegraDisponibilidade>())
        );

        var diario = new Diario(
            Id: "diario:1",
            TurmaId: turma.Id,
            ProfessorId: professor.Id,
            DisciplinaId: "disc:1",
            QuantidadeMaximaSemana: maxAulasSemana,
            QuantidadeMaximaTotal: 100
        );

        return new GeradorPayload(
            DataInicial: data,
            DataFinal: data,
            Turmas: new[] { turma },
            Professores: new[] { professor },
            Diarios: new[] { diario },
            HorariosDeAula: slots
        );
    }

    [Test]
    public void GerarHorario_SimpleCase_ReturnsOneAula()
    {
        var data = new DateOnly(2025, 1, 6); // Monday
        var slot = new SlotDeTempo("08:00:00", "08:50:00");
        var payload = BuildBasicPayload(data, new[] { slot });

        Console.WriteLine(TimetableJson.Stringify(payload));

        var horarios = Gerador.GerarHorario(payload);
        var primeiro = horarios.FirstOrDefault();

        Assert.That(primeiro, Is.Not.Null, "Deveria gerar ao menos um horário");
        Assert.That(primeiro!.Aulas.Length, Is.EqualTo(1), "Deveria agendar 1 aula no cenário básico");

        var aula = primeiro.Aulas[0];
        Assert.Multiple(() =>
        {
            Assert.That(aula.TurmaId, Is.EqualTo("turma:1"));
            Assert.That(aula.DiarioId, Is.EqualTo("diario:1"));
            Assert.That(aula.ProfessorId, Is.EqualTo("prof:1"));
            Assert.That(aula.Data, Is.EqualTo(data));
            Assert.That(aula.HorarioDeAula, Is.EqualTo(slot));
        });
    }

    [Test]
    public void GerarHorario_IndisponibilidadeDiaDaSemana_CurrentBehavior_AllowsAula()
    {
        var data = new DateOnly(2025, 1, 6); // Monday
        var slot = new SlotDeTempo("08:00:00", "08:50:00");

        var indisponibilidadeDiaTodo = new RegraDisponibilidade(new IRegraDisponibilidade[]
        {
            new RegraIndisponibilidadeDiaDaSemana(DayOfWeek.Monday, new SlotDeTempo("00:00:00","23:59:59"))
        });

        // Professor com regra de "indisponibilidade" na segunda o dia todo.
        // O comportamento atual do avaliador considera true quando o slot está dentro da janela configurada.
        var payload = BuildBasicPayload(
            data,
            new[] { slot },
            turmaDispon: new RegraDisponibilidade(Array.Empty<IRegraDisponibilidade>()),
            profDispon: indisponibilidadeDiaTodo,
            maxAulasSemana: 1
        );

        var horarios = Gerador.GerarHorario(payload);
        var primeiro = horarios.FirstOrDefault();

        Assert.That(primeiro, Is.Not.Null, "Deveria gerar um horário");
        Assert.That(primeiro!.Aulas.Length, Is.EqualTo(1), "Comportamento atual permite agendamento dentro da janela marcada como 'indisponível'");
    }
}
