namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Exceptions;

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
