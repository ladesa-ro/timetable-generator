using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Domain.Entities;

public record TimetableGridSchedule(
    string GroupId,
    string DiaryId,
    string TeacherId,
    DateOnly Date,
    TimeSlot TimeSlot
);