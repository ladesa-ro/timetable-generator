namespace Ladesa.TimetableGenerator.Domain.Models.Constraints;

/// <summary>
///     CONSTRAINT: For the same group and date, no overlapping time slots may be scheduled.
/// </summary>
public interface IConstraintGroupNoOverlappingTimeSlots : IConstraint;
