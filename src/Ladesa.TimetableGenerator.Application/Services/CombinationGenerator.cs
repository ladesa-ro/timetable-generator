using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Extensions;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.Schedule;

namespace Ladesa.TimetableGenerator.Application.Services;

/// <summary>
///     Generates all possible schedule combinations from the request,
///     with optional availability filtering.
/// </summary>
public class CombinationGenerator : ICombinationGenerator
{
    private static IEnumerable<Schedule> GetAllPossibleCombinations(
        GenerateTimetableCommand timetableCommand)
    {
        return from date in timetableCommand.GetDates()
            from timeSlot in timetableCommand.TimeSlots
            from grp in timetableCommand.Groups
            from diary in timetableCommand.DiaryFindByGroupId(grp.Id)
            select new Schedule(
                grp.Id, diary.Id, diary.TeacherId, date, timeSlot);
    }

    public IEnumerable<Schedule> GetAllCombinationsWithAvailability(
        GenerateTimetableCommand generateTimetableCommand,
        IAvailabilityEvaluator availabilityEvaluator)
    {
        foreach (var combination in GetAllPossibleCombinations(generateTimetableCommand))
        {
            var group = generateTimetableCommand.GroupFindByIdStrict(combination.GroupId);
            var teacher = generateTimetableCommand.TeacherFindByIdStrict(combination.TeacherId);

            if (group.Availability.IsAvailable(combination.Date, combination.TimeSlot, availabilityEvaluator)
                && teacher.Availability.IsAvailable(combination.Date, combination.TimeSlot, availabilityEvaluator))
            {
                yield return combination;
            }
        }
    }
}
