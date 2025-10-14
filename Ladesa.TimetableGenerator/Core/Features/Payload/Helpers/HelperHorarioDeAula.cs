using Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperHorarioDeAula
{
    public static SlotDeTempo? ByIndex(IGeradorPayload payload, int horarioDeAulaIndex)
    {
        var horarioDeAula = payload.HorariosDeAula[horarioDeAulaIndex];
        return horarioDeAula;
    }

    public static SlotDeTempo ByIndexStrict(
        IGeradorPayload payload,
        int horarioDeAulaIndex
    )
    {
        var horarioDeAula = ByIndex(payload, horarioDeAulaIndex);

        if (horarioDeAula == null)
        {
            throw new Exception($"Horário de aula não encontrado: índice {horarioDeAulaIndex}.");
        }

        return horarioDeAula;
    }
}
