namespace Ladesa.TimetableGenerator.v1.Core.Domain;

public record TimetableGridSchedule(
    string GroupId,
    string DiaryId,
    string TeacherId,
    DateOnly Date,
    TimeSlot TimeSlot
);