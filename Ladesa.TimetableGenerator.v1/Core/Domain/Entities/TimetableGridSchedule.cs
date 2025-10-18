using Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.v1.Core.Domain.Entities;

public record TimetableGridSchedule(
    string GroupId,
    string DiaryId,
    string TeacherId,
    DateOnly Date,
    TimeSlot TimeSlot
);