namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

public interface ITimetableOptimizer
{
    void OptimizeResult(GenerationContext context, long? scoreLimit = null);
}
