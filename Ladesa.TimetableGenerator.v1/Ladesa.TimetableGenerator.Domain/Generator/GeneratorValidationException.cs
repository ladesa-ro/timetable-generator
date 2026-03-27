namespace Ladesa.TimetableGenerator.Domain.Generator;

public enum GeneratorValidationErrorCode
{
    DiaryReferencesNotFound,
    GroupNotFound,
    TeacherNotFound,
    DuplicateGroupId,
    DuplicateTeacherId,
    DuplicateDiaryId,
    InvalidRRule
}

public class GeneratorValidationException : Exception
{
    public GeneratorValidationException(GeneratorValidationErrorCode code, string message, string? details = null)
        : base(message)
    {
        Code = code;
        Details = details;
    }

    public GeneratorValidationErrorCode Code { get; }
    public string? Details { get; }
}