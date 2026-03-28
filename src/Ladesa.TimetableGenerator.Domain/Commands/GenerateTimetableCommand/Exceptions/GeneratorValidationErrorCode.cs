namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;

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