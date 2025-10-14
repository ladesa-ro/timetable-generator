namespace Ladesa.TimetableGenerator.Core.Domain;

public record Diario(
    string Id,
    string TurmaId,
    string ProfessorId,
    string DisciplinaId,
    int QuantidadeMaximaSemana,
    int QuantidadeMaximaTotal = 100
);
