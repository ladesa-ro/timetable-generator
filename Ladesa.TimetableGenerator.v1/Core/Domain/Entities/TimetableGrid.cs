using Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.v1.Core.Domain.Entities;

public record TimetableGrid(
    DateOnly DateStart,
    DateOnly DateEnd,
    TimeSlot[] TimeSlots,
    TimetableGridSchedule[] Schedules
);