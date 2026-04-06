namespace Ladesa.TimetableGenerator.Domain.Models.TimeSlot;

/// <summary>
/// Constants for common time slots used in scheduling constraints.
/// </summary>
public static class TimeSlotConstants
{
    public static readonly TimeSlot MorningShift = new(new TimeOnly(0, 0, 0), new TimeOnly(11, 59, 59));
    public static readonly TimeSlot AfternoonShift = new(new TimeOnly(12, 0, 0), new TimeOnly(17, 59, 59));
    public static readonly TimeSlot NightShift = new(new TimeOnly(18, 0, 0), new TimeOnly(23, 59, 59));
    public static readonly TimeSlot LunchBufferBefore = new(new TimeOnly(11, 30, 0), new TimeOnly(12, 0, 0));
    public static readonly TimeSlot LunchBufferAfter = new(new TimeOnly(13, 0, 0), new TimeOnly(13, 30, 0));
}
