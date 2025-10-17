using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperHorarioDeAula
{
    public static TimeSlot? ByIndex(GeneratorPayload payload, int horarioDeAulaIndex)
    {
        var horarioDeAula = payload.TimeSlots[horarioDeAulaIndex];
        return horarioDeAula;
    }

    public static TimeSlot ByIndexStrict(GeneratorPayload payload, int horarioDeAulaIndex)
    {
        var horarioDeAula = ByIndex(payload, horarioDeAulaIndex);

        if (horarioDeAula == null)
            throw new Exception($"Horário de aula não encontrado: índice {horarioDeAulaIndex}.");

        return horarioDeAula;
    }
}