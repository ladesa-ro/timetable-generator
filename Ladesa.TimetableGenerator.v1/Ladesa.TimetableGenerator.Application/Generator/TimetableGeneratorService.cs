using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Application.Generator;

public class TimetableGeneratorService : ITimetableGeneratorService
{
    public IEnumerable<GeneratedTimetable> Generate(GenerateRequest request)
        => Domain.Generator.Generator.GenerateTimetables(request);
}
