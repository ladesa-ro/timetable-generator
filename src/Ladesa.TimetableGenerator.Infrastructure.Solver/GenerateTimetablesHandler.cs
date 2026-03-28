using Ladesa.TimetableGenerator.Application.Services;
using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver;

public class GenerateTimetablesHandler : ITimetableSolver
{
    private readonly IGenerator _generator;
    private readonly IAvailabilityEvaluator _availabilityEvaluator;

    public GenerateTimetablesHandler(
        IGenerator generator,
        IAvailabilityEvaluator availabilityEvaluator)
    {
        _generator = generator;
        _availabilityEvaluator = availabilityEvaluator;
    }

    public IEnumerable<GenerateTimetableCommandResponse> Solve(GenerateTimetableCommand timetableCommand)
        => _generator.GenerateTimetables(timetableCommand, _availabilityEvaluator);
}
