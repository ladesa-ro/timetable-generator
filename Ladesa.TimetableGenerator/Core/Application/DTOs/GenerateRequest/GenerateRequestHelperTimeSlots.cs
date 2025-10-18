using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Application.DTOs.GenerateRequest;

public static class GenerateRequestHelperTimeSlots
{
    public static TimeSlot? TimeSlotByIndex(this GenerateRequest payload, int horarioDeAulaIndex)
    {
        var horarioDeAula = payload.TimeSlots[horarioDeAulaIndex];
        return horarioDeAula;
    }

    public static TimeSlot TimeSlotByIndexStrict(this GenerateRequest payload, int horarioDeAulaIndex)
    {
        var horarioDeAula = payload.TimeSlotByIndex(horarioDeAulaIndex);

        if (horarioDeAula == null)
            throw new Exception($"Time slot not found: by index {horarioDeAulaIndex}.");

        return horarioDeAula;
    }
}