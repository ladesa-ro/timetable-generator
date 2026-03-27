namespace Ladesa.TimetableGenerator.Domain.Models.Constraints;

/// <summary>
///     CONSTRAINT: For the same teacher and date, no overlapping time slots may be scheduled.
/// </summary>
public interface IConstraintTeacherNoOverlappingTimeSlots : IConstraint;
