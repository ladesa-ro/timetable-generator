using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

public record GeradorPayload(
    Guid RequestId,
    DateOnly DataInicial,
    DateOnly DataFinal,
    Turma[] Turmas,
    Professor[] Professores,
    Diario[] Diarios,
    SlotDeTempo[] HorariosDeAula
);
