using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;

public record GeneratorPayload(
    Guid RequestId,
    DateOnly DateStart,
    DateOnly DateEnd,
    Group[] Groups,
    Teacher[] Teachers,
    Diary[] Diaries,
    TimeSlot[] TimeSlots
);