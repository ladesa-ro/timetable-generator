using Ladesa.TimetableGenerator.Application.Abstractions;
using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Domain.Commands;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;
using Ladesa.TimetableGenerator.Domain.Generator.GenerateRequest;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver;

public class TimetableGeneratorService : ITimetableGeneratorService
{
    private readonly IGenerator _generator;
    private readonly IAvailabilityEvaluator _availabilityEvaluator;

    public TimetableGeneratorService(
        IGenerator generator,
        IAvailabilityEvaluator availabilityEvaluator)
    {
        _generator = generator;
        _availabilityEvaluator = availabilityEvaluator;
    }

    public IEnumerable<GenerateTimetableCommandResponse> Generate(GenerateTimetableCommand timetableCommand)
        => _generator.GenerateTimetables(timetableCommand, _availabilityEvaluator);
}
