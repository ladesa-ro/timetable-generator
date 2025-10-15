using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

public record HorarioGerado(
    Guid RequestId,
    DateOnly DataInicial,
    DateOnly DataFinal,
    SlotDeTempo[] HorariosDeAula,
    HorarioGeradoAula[] Aulas,
    int? Score
);