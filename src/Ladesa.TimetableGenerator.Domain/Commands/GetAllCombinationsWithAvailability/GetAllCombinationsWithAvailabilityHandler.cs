using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Extensions;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Domain.Commands.GetAllCombinationsWithAvailability;

/// <summary>
///     Generates all possible schedule combinations from the request,
///     with optional availability filtering.
/// </summary>
public class GetAllCombinationsWithAvailabilityHandler : IGetAllCombinationsWithAvailabilityHandler
{
    private static IEnumerable<TimetableGridSchedule> GetAllPossibleCombinations(
        GenerateTimetableCommand.GenerateTimetableCommand timetableCommand)
    {
        return from date in timetableCommand.GetDates()
            from timeSlot in timetableCommand.TimeSlots
            from grp in timetableCommand.Groups
            from diary in timetableCommand.DiaryFindByGroupId(grp.Id)
            select new TimetableGridSchedule(
                grp.Id, diary.Id, diary.TeacherId, date, timeSlot);
    }

    public IEnumerable<TimetableGridSchedule> GetAllCombinationsWithAvailability(
        GenerateTimetableCommand.GenerateTimetableCommand generateTimetableCommand,
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
