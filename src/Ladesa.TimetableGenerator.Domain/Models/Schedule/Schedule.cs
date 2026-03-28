namespace Ladesa.TimetableGenerator.Domain.Models.Schedule;

public record Schedule(
    string GroupId,
    string DiaryId,
    string TeacherId,
    DateOnly Date,
    TimeSlot.TimeSlot TimeSlot
);