using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;

public record GenerateTimetableCommandResponse(
    TimetableGrid Timetable,
    int Score
);
