using Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Core;

namespace Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Constraints;

public interface IGeneratorConstraint
{
    public static abstract void Apply(GenerationContext generationContext);
}