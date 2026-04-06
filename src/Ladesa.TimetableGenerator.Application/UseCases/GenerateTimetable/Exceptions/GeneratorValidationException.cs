namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Exceptions;

public class GeneratorValidationException(GeneratorValidationErrorCode code, string message, string? details = null)
    : Exception(message)
{
    public GeneratorValidationErrorCode Code { get; } = code;
    public string? Details { get; } = details;
}
