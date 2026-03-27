using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Domain.Constants;

/// <summary>
/// Constants for common time slots used in scheduling constraints.
/// </summary>
public static class TimeSlotConstants
{
    /// <summary>
    /// Morning shift: 00:00 to 11:59:59
    /// </summary>
    public static readonly TimeSlot MorningShift = new("00:00:00", "11:59:59");

    /// <summary>
    /// Afternoon shift: 12:00 to 17:59:59
    /// </summary>
    public static readonly TimeSlot AfternoonShift = new("12:00:00", "17:59:59");

    /// <summary>
    /// Night shift: 18:00 to 23:59:59
    /// </summary>
    public static readonly TimeSlot NightShift = new("18:00:00", "23:59:59");

    /// <summary>
    /// Buffer period before lunch (11:30 to 12:00).
    /// Used to ensure minimum lunch break of 1:30.
    /// </summary>
    public static readonly TimeSlot LunchBufferBefore = new("11:30:00", "12:00:00");

    /// <summary>
    /// Buffer period after lunch (13:00 to 13:30).
    /// Used to ensure minimum lunch break of 1:30.
    /// </summary>
    public static readonly TimeSlot LunchBufferAfter = new("13:00:00", "13:30:00");
}
