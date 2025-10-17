using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Logic;

public static class SlotDeTempoEvaluator
{
    public static bool VerificarIntervalo(TimeSlot timeSlot, TimeSpan horario)
    {
        var horarioInicio = TimeSpan.Parse(timeSlot.Start);
        var horarioFim = TimeSpan.Parse(timeSlot.End);
        return horarioInicio <= horario && horario <= horarioFim;
    }

    public static bool VerificarIntervalo(TimeSlot timeSlot, string horario)
    {
        var horarioConvertido = TimeSpan.Parse(horario);
        return VerificarIntervalo(timeSlot, horarioConvertido);
    }

    public static bool VerificarIntervalo(TimeSlot timeSlot, TimeSlot intervalo2)
    {
        return VerificarIntervalo(timeSlot, intervalo2.Start)
               && VerificarIntervalo(timeSlot, intervalo2.End);
    }
}