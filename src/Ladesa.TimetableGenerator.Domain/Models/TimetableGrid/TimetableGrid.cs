namespace Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

public record TimetableGrid(
    DateOnly DateStart,
    DateOnly DateEnd,

    TimeSlot.TimeSlot[] TimeSlots,
    TimetableGridSchedule[] Schedules
);
