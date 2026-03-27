namespace Ladesa.TimetableGenerator.Domain.Models;

public interface ITimetableOptimizer
{
    void OptimizeResult(IGenerationContext context, long? scoreLimit = null);
}
