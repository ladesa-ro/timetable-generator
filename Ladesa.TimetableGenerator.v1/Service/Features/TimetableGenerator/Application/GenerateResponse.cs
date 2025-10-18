using Ladesa.TimetableGenerator.v1.Core.Domain.Entities;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application;

public record GenerateResponse(
    //
    bool Success,
    string Message,
    //
    GeneratedTimetable[] GeneratedTimetables,
    DateOnly Date
);