namespace Ladesa.TimetableGenerator.Domain.Models.Constraints;

/// <summary>
///     CONSTRAINT: Group - no more than one schedule at the same time.
/// </summary>
public interface IConstraintGroupOneScheduleAtSameTime : IConstraint;
