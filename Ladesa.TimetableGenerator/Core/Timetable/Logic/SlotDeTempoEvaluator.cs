using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Logic;

public static class SlotDeTempoEvaluator
{
    public static bool VerificarIntervalo(SlotDeTempo slotDeTempo, TimeSpan horario)
    {
        var horarioInicio = TimeSpan.Parse(slotDeTempo.HorarioInicio);
        var horarioFim = TimeSpan.Parse(slotDeTempo.HorarioFim);
        return horarioInicio <= horario && horario <= horarioFim;
    }

    public static bool VerificarIntervalo(SlotDeTempo slotDeTempo, string horario)
    {
        var horarioConvertido = TimeSpan.Parse(horario);
        return VerificarIntervalo(slotDeTempo, horarioConvertido);
    }

    public static bool VerificarIntervalo(
        SlotDeTempo slotDeTempo,
        SlotDeTempo intervalo2
    )
    {
        return VerificarIntervalo(slotDeTempo, intervalo2.HorarioInicio)
               && VerificarIntervalo(slotDeTempo, intervalo2.HorarioFim);
    }
}