using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.Schedule;

namespace Ladesa.TimetableGenerator.Application.Services;

public interface ICombinationGenerator
{
    IEnumerable<Schedule> GetAllCombinationsWithAvailability(
        GenerateTimetableCommand command,
        IAvailabilityEvaluator availabilityEvaluator);
}
