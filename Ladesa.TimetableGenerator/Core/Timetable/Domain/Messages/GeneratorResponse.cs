using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;

public record GeneratorResponse(
    bool Success,
    string Message,
    GeneratedTimetable[] GeneratedTimetables,
    DateOnly Date
);