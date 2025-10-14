using Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperTurmas
{
    public static Turma? FindById(IGeradorPayload payload, string turmaId)
    {
        var turma = payload.Turmas.ToList().Find(turma => turma.Id == turmaId);
        return turma;
    }

    public static Turma FindByIdStrict(IGeradorPayload payload, string turmaId, string? exceptionContext = null)
    {
        var turma = FindById(payload, turmaId);

        if (turma == null) throw new Exception($"Turma não encontrada: {turmaId}{exceptionContext}.");
        ;

        return turma;
    }
}