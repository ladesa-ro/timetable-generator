namespace Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

public record Diario(
    string Id,
    string TurmaId,
    string ProfessorId,
    string DisciplinaId,
    int QuantidadeMaximaSemana,
    int QuantidadeMaximaTotal = 100
);