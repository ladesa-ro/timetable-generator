using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

public record HorarioGeradoAula(
    string TurmaId,
    string DiarioId,
    string ProfessorId,
    DateOnly Data,
    SlotDeTempo HorarioDeAula
);
