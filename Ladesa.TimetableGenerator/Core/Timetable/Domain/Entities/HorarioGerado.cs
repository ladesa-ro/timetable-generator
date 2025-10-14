using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

public record HorarioGerado(
    DateOnly DataInicial,
    DateOnly DataFinal,
    SlotDeTempo[] HorariosDeAula,
    HorarioGeradoAula[] Aulas
);