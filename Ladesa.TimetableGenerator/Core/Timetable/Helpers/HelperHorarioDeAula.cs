using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperHorarioDeAula
{
    public static SlotDeTempo? ByIndex(GeradorPayload payload, int horarioDeAulaIndex)
    {
        var horarioDeAula = payload.HorariosDeAula[horarioDeAulaIndex];
        return horarioDeAula;
    }

    public static SlotDeTempo ByIndexStrict(GeradorPayload payload, int horarioDeAulaIndex)
    {
        var horarioDeAula = ByIndex(payload, horarioDeAulaIndex);

        if (horarioDeAula == null)
            throw new Exception($"Horário de aula não encontrado: índice {horarioDeAulaIndex}.");

        return horarioDeAula;
    }
}
