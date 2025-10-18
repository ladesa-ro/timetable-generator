namespace Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

public record TimeSlot(string Start, string End)
{
    public bool Verify(TimeSpan horario)
    {
        var horarioInicio = TimeSpan.Parse(Start);
        var horarioFim = TimeSpan.Parse(End);
        return horarioInicio <= horario && horario <= horarioFim;
    }

    public bool Verify(string horario)
    {
        var horarioConvertido = TimeSpan.Parse(horario);
        return Verify(horarioConvertido);
    }

    public bool Verify(TimeSlot intervalo2)
    {
        return Verify(intervalo2.Start)
               && Verify(intervalo2.End);
    }
};