using Ladesa.TimetableGenerator.v1.Core.Generator;

namespace Ladesa.TimetableGenerator.v1.Core.Constraints;

public interface IGeneratorConstraint
{
    public static abstract void Apply(GenerationContext generationContext);
}