namespace Ladesa.TimetableGenerator.v1.Core.Domain;

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

    public override string ToString()
    {
        return $"[{Start} - {End}]";
    }

    public DateTime GetDateTimeStart(DateOnly date)
    {
        return date.ToDateTime(TimeOnly.Parse(Start));
    }
    
    public DateTime GetDateTimeEnd(DateOnly date)
    {
        return date.ToDateTime(TimeOnly.Parse(End));
    }

    public (DateTime, DateTime) GetDateTimeRange(DateOnly date)
    {
        return (GetDateTimeStart(date), GetDateTimeEnd(date));
    }

    public TimeSpan Distance(TimeSlot timeSlot2)
    {
        var sameStart = this.Start == timeSlot2.Start;
        var sameEnd = this.End == timeSlot2.End;

        return sameStart switch
        {
            true when sameEnd => TimeSpan.Zero,
            true => TimeSpan.Parse(this.End) - TimeSpan.Parse(timeSlot2.End),
            _ => TimeSpan.Parse(this.Start) - TimeSpan.Parse(timeSlot2.Start)
        };
    }
};