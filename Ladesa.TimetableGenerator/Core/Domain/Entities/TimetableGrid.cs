using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Domain.Entities;

public record TimetableGrid(
    DateOnly DateStart,
    DateOnly DateEnd,
    TimeSlot[] TimeSlots,
    TimetableGridSchedule[] Schedules
);