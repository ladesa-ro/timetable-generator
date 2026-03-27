using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver;

public class TimetableGeneratorService : ITimetableGeneratorService
{
    public IEnumerable<GeneratedTimetable> Generate(GenerateRequest request)
        => Generator.Generator.GenerateTimetables(request);
}
