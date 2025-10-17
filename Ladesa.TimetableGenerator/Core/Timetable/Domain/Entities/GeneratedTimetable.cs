using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

public record GeneratedTimetable(
    Guid RequestId,
    DateOnly DateStart,
    DateOnly DateEnd,
    TimeSlot[] TimeSlots,
    GeneratedTimetableLesson[] Schedules,
    int Score
);