namespace Ladesa.TimetableGenerator.Core.Domain;

public record IntervaloDeTempo(string HorarioInicio, string HorarioFim)
{
    public override string ToString()
    {
        return $"[{HorarioInicio} - {HorarioFim}]";
    }

    public static bool VerificarIntervalo(IntervaloDeTempo intervaloDeTempo, TimeSpan horario)
    {
        var horarioInicio = TimeSpan.Parse(intervaloDeTempo.HorarioInicio);
        var horarioFim = TimeSpan.Parse(intervaloDeTempo.HorarioFim);
        return (horarioInicio <= horario) && (horario <= horarioFim);
    }

    public static bool VerificarIntervalo(IntervaloDeTempo intervaloDeTempo, string horario)
    {
        var horarioConvertido = TimeSpan.Parse(horario);
        return VerificarIntervalo(intervaloDeTempo, horarioConvertido);
    }

    public static bool VerificarIntervalo(IntervaloDeTempo intervaloDeTempo, IntervaloDeTempo intervalo2)
    {
        return VerificarIntervalo(intervaloDeTempo, intervalo2.HorarioInicio)
            && VerificarIntervalo(intervaloDeTempo, intervalo2.HorarioFim);
    }
}
