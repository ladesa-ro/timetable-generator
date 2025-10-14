using Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperDiarios
{
    public static Diario? FindById(IGeradorPayload payload, string diarioId)
    {
        var diario = payload.Diarios.ToList().Find(diario => diario.Id == diarioId);
        return diario;
    }

    public static Diario FindByIdStrict(IGeradorPayload payload, string diarioId, string? exceptionContext = null)
    {
        var diario = FindById(payload, diarioId);

        if (diario == null) throw new Exception($"Diário não encontrado: {diarioId}{exceptionContext}.");
        ;

        return diario;
    }

    public static IEnumerable<Diario> ByTurmaId(IGeradorPayload payload, string turmaId)
    {
        return payload.Diarios.Where(diario => diario.TurmaId == turmaId).ToList();
    }
}