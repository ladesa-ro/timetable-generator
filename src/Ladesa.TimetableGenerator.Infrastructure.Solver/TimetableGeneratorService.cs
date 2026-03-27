using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver;

public class TimetableGeneratorService : ITimetableGeneratorService
{
    private readonly Generator.Generator _generator;
    private readonly IAvailabilityEvaluator _availabilityEvaluator;

    public TimetableGeneratorService(
        Generator.Generator generator,
        IAvailabilityEvaluator availabilityEvaluator)
    {
        _generator = generator;
        _availabilityEvaluator = availabilityEvaluator;
    }

    public IEnumerable<GeneratedTimetable> Generate(GenerateRequest request)
        => _generator.GenerateTimetables(request, _availabilityEvaluator);
}
