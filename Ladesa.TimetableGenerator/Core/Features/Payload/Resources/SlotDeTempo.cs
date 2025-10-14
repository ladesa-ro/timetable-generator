namespace Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

public record SlotDeTempo(string HorarioInicio, string HorarioFim)
{
    public override string ToString()
    {
        return $"[{HorarioInicio} - {HorarioFim}]";
    }

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