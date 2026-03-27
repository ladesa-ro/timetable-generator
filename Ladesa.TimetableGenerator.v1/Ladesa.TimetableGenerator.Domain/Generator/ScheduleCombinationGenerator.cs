using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Domain.Generator;

/// <summary>
///     Generates all possible schedule combinations from the request,
///     with optional availability filtering.
/// </summary>
public static class ScheduleCombinationGenerator
{
    /// <summary>
    ///     Generates all possible combinations (date x timeslot x group x diary)
    ///     without applying any constraints.
    /// </summary>
    internal static IEnumerable<GenerationScheduleCombination> GetAllPossibleCombinations(
        GenerateRequest request)
    {
        return from date in request.GetDates()
            from timeSlot in request.TimeSlots
            from grp in request.Groups
            from diary in request.DiaryFindByGroupId(grp.Id)
            select new GenerationScheduleCombination(
                date, timeSlot, grp.Id, diary.Id, diary.TeacherId);
    }

    /// <summary>
    ///     Generates all possible combinations, filtering by
    ///     group and teacher availability rules.
    /// </summary>
    public static IEnumerable<GenerationScheduleCombination> GetAllCombinationsWithAvailability(
        GenerateRequest generateRequest)
    {
        foreach (var combination in GetAllPossibleCombinations(generateRequest))
        {
            var group = generateRequest.GroupFindByIdStrict(combination.GroupId);
            var teacher = generateRequest.TeacherFindByIdStrict(combination.TeacherId);

            if (group.Availability.IsAvailable(combination.Date, combination.TimeSlot)
                && teacher.Availability.IsAvailable(combination.Date, combination.TimeSlot))
                yield return combination;
        }
    }
}
