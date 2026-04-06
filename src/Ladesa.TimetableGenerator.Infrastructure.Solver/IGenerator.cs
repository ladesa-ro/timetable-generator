using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver;

public interface IGenerator
{
    IEnumerable<GenerateTimetableCommandResponse> GenerateTimetables(
        GenerateTimetableCommand timetableCommand,
        IAvailabilityEvaluator availabilityEvaluator);
}
