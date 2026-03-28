using Ladesa.TimetableGenerator.Application.Services;

namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;

public class GenerateTimetableUseCase(ITimetableSolver solver) : IGenerateTimetableUseCase
{
    private readonly ITimetableSolver _solver = solver ?? throw new ArgumentNullException(nameof(solver));

    public Task<GenerateTimetableCommandResponse> HandleAsync(GenerateTimetableCommand command)
    {
        GenerateTimetableCommandValidator.Validate(command);

        var responses = _solver.Solve(command);

        var first = responses.First();

        return Task.FromResult(first);
    }
}
