namespace Ladesa.TimetableGenerator.Domain.Models.Constraints;

/// <summary>
///     CONSTRAINT: Group - no schedules in lunchtime - at least 01:30.
/// </summary>
public interface IConstraintGroupLunch : IConstraint;
