using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;

public record GenerateTimetableCommandResponse(
    TimetableGrid Timetable,
    int Score
);
