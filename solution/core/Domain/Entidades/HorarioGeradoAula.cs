namespace Ladesa.TimetableGenerator.Core.Domain;

public record HorarioGeradoAula(
    string TurmaId,
    string DiarioId,
    string ProfessorId,
    DateOnly Data,
    IntervaloDeTempo IntervaloDeTempo
);