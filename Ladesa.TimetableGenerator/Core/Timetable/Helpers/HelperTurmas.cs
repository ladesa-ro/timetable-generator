using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperTurmas
{
    public static Group? FindById(GeneratorPayload payload, string turmaId)
    {
        var turma = payload.Groups.ToList().Find(turma => turma.Id == turmaId);
        return turma;
    }

    public static Group FindByIdStrict(
        GeneratorPayload payload,
        string turmaId,
        string? exceptionContext = null
    )
    {
        var turma = FindById(payload, turmaId);

        if (turma == null)
            throw new Exception($"Turma não encontrada: {turmaId}{exceptionContext}.");

        return turma;
    }
}