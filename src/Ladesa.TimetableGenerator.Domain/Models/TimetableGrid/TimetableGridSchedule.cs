namespace Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

public record TimetableGridSchedule(
    string GroupId,
    string DiaryId,
    string TeacherId,
    DateOnly Date,
    TimeSlot.TimeSlot TimeSlot
);