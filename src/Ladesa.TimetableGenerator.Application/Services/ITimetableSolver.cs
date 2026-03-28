using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;

namespace Ladesa.TimetableGenerator.Application.Services;

public interface ITimetableSolver
{
    IEnumerable<GenerateTimetableCommandResponse> Solve(GenerateTimetableCommand timetableCommand);
}
