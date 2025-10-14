using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperTurmas
{
    public static Turma? FindById(GeradorPayload payload, string turmaId)
    {
        var turma = payload.Turmas.ToList().Find(turma => turma.Id == turmaId);
        return turma;
    }

    public static Turma FindByIdStrict(GeradorPayload payload, string turmaId, string? exceptionContext = null)
    {
        var turma = FindById(payload, turmaId);

        if (turma == null) throw new Exception($"Turma não encontrada: {turmaId}{exceptionContext}.");

        return turma;
    }
}