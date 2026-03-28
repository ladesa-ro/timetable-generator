using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Domain.Commands.GetAllCombinationsWithAvailability;

public interface IGetAllCombinationsWithAvailabilityHandler
{
    IEnumerable<TimetableGridSchedule> GetAllCombinationsWithAvailability(
        GenerateTimetableCommand.GenerateTimetableCommand generateTimetableCommand,
        IAvailabilityEvaluator availabilityEvaluator);
}
