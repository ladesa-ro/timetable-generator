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
    }

    private static void ValidateTimeSlots(GenerateTimetableCommand command)
    {
        foreach (var slot in command.TimeSlots)
        {
            if (slot.Start >= slot.End)
                throw new ArgumentException("Invalid time slot: start must be before end within the same day.");
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
