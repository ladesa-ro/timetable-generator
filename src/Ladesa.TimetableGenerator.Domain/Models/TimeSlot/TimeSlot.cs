namespace Ladesa.TimetableGenerator.Domain.Models.TimeSlot;

/// <summary>
///     Represents a time slot defined by start and end times.
/// </summary>
public record TimeSlot(TimeOnly Start, TimeOnly End)
{
    /// <summary>Checks whether the given time falls within this slot.</summary>
    public bool Contains(TimeOnly time)
    {
        return Start <= time && time <= End;
    }

    /// <summary>Checks whether the given time falls within this slot.</summary>
    public bool Contains(TimeSpan time)
    {
        return Contains(TimeOnly.FromTimeSpan(time));
    }

    /// <summary>Checks whether the other slot is fully contained within this slot.</summary>
    public bool Contains(TimeSlot other)
    {
        return Contains(other.Start) && Contains(other.End);
    }

    public override string ToString()
    {
        return $"[{Start:HH:mm:ss} - {End:HH:mm:ss}]";
    }

    /// <summary>Returns the start time as a DateTime on the given date.</summary>
    public DateTime GetDateTimeStart(DateOnly date)
    {
        return date.ToDateTime(Start);
    }

    /// <summary>Returns the end time as a DateTime on the given date.</summary>
    public DateTime GetDateTimeEnd(DateOnly date)
    {
        return date.ToDateTime(End);
    }

    /// <summary>Returns the start and end as a DateTime tuple on the given date.</summary>
    public (DateTime, DateTime) GetDateTimeRange(DateOnly date)
    {
        return (GetDateTimeStart(date), GetDateTimeEnd(date));
    }

    /// <summary>Computes the signed time difference between this slot and another, comparing starts first.</summary>
    public TimeSpan Distance(TimeSlot other)
    {
        var thisStart = Start.ToTimeSpan();
        var otherStart = other.Start.ToTimeSpan();

        if (thisStart != otherStart)
            return thisStart - otherStart;

        var thisEnd = End.ToTimeSpan();
        var otherEnd = other.End.ToTimeSpan();
        return thisEnd - otherEnd;
    }
}
