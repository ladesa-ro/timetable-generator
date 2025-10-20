namespace Ladesa.TimetableGenerator.v1.Core.Domain;

public record TimetableGrid(
    DateOnly DateStart,
    DateOnly DateEnd,
    TimeSlot[] TimeSlots,
    TimetableGridSchedule[] Schedules
);