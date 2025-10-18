using Ladesa.TimetableGenerator.Core.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Application.DTOs;

public record GenerateResponse(
    Guid RequestId,
    //
    bool Success,
    string Message,
    //
    GeneratedTimetable[] GeneratedTimetables,
    DateOnly Date
);