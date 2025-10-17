using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

public record GeneratedTimetableLesson(
    string GroupId,
    string DiaryId,
    string TeacherId,
    DateOnly Date,
    TimeSlot TimeSlot
);