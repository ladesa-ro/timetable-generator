namespace Ladesa.TimetableGenerator.Domain.Models;

public interface IConstraint
{
    void Apply(IGenerationContext context);
}
