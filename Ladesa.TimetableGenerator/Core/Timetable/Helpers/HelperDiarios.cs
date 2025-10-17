using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperDiarios
{
    public static Diary? FindById(GeneratorPayload payload, string diarioId)
    {
        var diario = payload.Diaries.ToList().Find(diario => diario.Id == diarioId);
        return diario;
    }

    public static Diary FindByIdStrict(
        GeneratorPayload payload,
        string diarioId,
        string? exceptionContext = null
    )
    {
        var diario = FindById(payload, diarioId);

        if (diario == null)
            throw new Exception($"Diário não encontrado: {diarioId}{exceptionContext}.");
        ;

        return diario;
    }

    public static IEnumerable<Diary> ByTurmaId(GeneratorPayload payload, string turmaId)
    {
        return payload.Diaries.Where(diario => diario.GroupId == turmaId).ToList();
    }
}