namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

public record Diario(
    string Id,
    string TurmaId,
    string ProfessorId,
    string DisciplinaId,
    int QuantidadeMaximaSemana,
    int QuantidadeMaximaTotal = 100
);