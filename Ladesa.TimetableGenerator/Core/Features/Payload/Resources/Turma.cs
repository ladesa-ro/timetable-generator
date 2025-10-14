namespace Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

public record Turma(
    string Id,
    IRegraDisponibilidade RegraDisponibilidade
);
