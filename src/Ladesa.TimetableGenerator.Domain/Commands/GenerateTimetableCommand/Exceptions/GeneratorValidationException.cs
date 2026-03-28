namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;

public class GeneratorValidationException(GeneratorValidationErrorCode code, string message, string? details = null)
    : Exception(message)
{
    public GeneratorValidationErrorCode Code { get; } = code;
    public string? Details { get; } = details;
    
    public static void ValidateNoDuplicates<T>(
        T[] items,
        Func<T, string> idSelector,
        GeneratorValidationErrorCode errorCode,
        string entityName
    )
    {
        if (items.GroupBy(idSelector).Any(grouped => grouped.Count() > 1))
            throw new GeneratorValidationException(errorCode, $"Duplicate entity IDs found in {entityName}.");
    }
}