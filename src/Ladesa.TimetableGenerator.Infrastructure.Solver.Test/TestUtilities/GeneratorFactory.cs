using Ladesa.TimetableGenerator.Application.Services;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Test.TestUtilities;

/// <summary>
///     Factory for creating a fully-wired Generator instance for use in integration tests.
/// </summary>
public static class GeneratorFactory
{
    public static Solver.Generator.Generator CreateDefault()
    {
        var combinationGenerator = new CombinationGenerator();
        var optimizer = new TimetableOptimizer();
        return new Solver.Generator.Generator(combinationGenerator, optimizer);
    }
}
