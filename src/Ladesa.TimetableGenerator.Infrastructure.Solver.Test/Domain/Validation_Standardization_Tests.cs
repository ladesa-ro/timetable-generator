using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Exceptions;
using Ladesa.TimetableGenerator.Domain.Models.Availability;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.TimeSlot;
using Ladesa.TimetableGenerator.Infrastructure.Solver;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Test.Domain;

[TestFixture]
public class Validation_Standardization_Tests
{
    private readonly IAvailabilityEvaluator _evaluator = new IcalAvailabilityEvaluator();

    [Test]
    public void Availability_InvalidRRule_ShouldThrow_Standardized()
    {
        var invalidRule = new AvailabilityRuleUnavailability(
            RRule: "THIS_IS_NOT_A_VALID_RRULE",
            DateStart: new DateTime(2025, 10, 27, 8, 0, 0),
            DateEnd: new DateTime(2025, 10, 27, 12, 0, 0)
        );

        var date = new DateOnly(2025, 10, 27);
        var slot = new TimeSlot(new TimeOnly(9, 0, 0), new TimeOnly(10, 0, 0));

        var ex = Assert.Throws<GeneratorValidationException>(() => _evaluator.IsAvailable(invalidRule, date, slot));
        Assert.That(ex!.Code, Is.EqualTo(GeneratorValidationErrorCode.InvalidRRule));
        Assert.That(ex!.Message, Does.Contain("invalid RRULE"));
    }
}
