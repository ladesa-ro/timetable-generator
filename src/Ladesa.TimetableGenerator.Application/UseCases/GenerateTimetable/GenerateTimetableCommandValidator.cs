using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Exceptions;

namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;

public static class GenerateTimetableCommandValidator
{
    public static void Validate(GenerateTimetableCommand command)
    {
        ValidateTimeSlots(command);
        ValidateNoDuplicates(command.Groups, g => g.Id,
            GeneratorValidationErrorCode.DuplicateGroupId, "Groups");
        ValidateNoDuplicates(command.Teachers, t => t.Id,
            GeneratorValidationErrorCode.DuplicateTeacherId, "Teachers");
        ValidateNoDuplicates(command.Diaries, d => d.Id,
            GeneratorValidationErrorCode.DuplicateDiaryId, "Diaries");
        ValidateDiaryReferences(command);
    }

    private static void ValidateTimeSlots(GenerateTimetableCommand command)
    {
        foreach (var slot in command.TimeSlots)
        {
            if (slot.Start >= slot.End)
                throw new ArgumentException("Invalid time slot: start must be before end within the same day.");
        }
    }

    private static void ValidateDiaryReferences(GenerateTimetableCommand command)
    {
        if (command.Diaries is null) return;

        var groupIds = new HashSet<string>(command.Groups.Select(g => g.Id));
        var teacherIds = new HashSet<string>(command.Teachers.Select(t => t.Id));

        foreach (var diary in command.Diaries)
        {
            if (!groupIds.Contains(diary.GroupId) && !teacherIds.Contains(diary.TeacherId))
                throw new GeneratorValidationException(GeneratorValidationErrorCode.DiaryReferencesNotFound, "Diary references not found: group and teacher not found.");
            if (!groupIds.Contains(diary.GroupId))
                throw new GeneratorValidationException(GeneratorValidationErrorCode.GroupNotFound, $"Group not found: {diary.GroupId}.");
            if (!teacherIds.Contains(diary.TeacherId))
                throw new GeneratorValidationException(GeneratorValidationErrorCode.TeacherNotFound, $"Teacher not found: {diary.TeacherId}.");
        }
    }

    public static void ValidateNoDuplicates<T>(
        T[] items,
        Func<T, string> idSelector,
        GeneratorValidationErrorCode errorCode,
        string entityName)
    {
        if (items.GroupBy(idSelector).Any(grouped => grouped.Count() > 1))
            throw new GeneratorValidationException(errorCode, $"Duplicate entity IDs found in {entityName}.");
    }
}
