namespace Ladesa.TimetableGenerator.Domain.Models.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no schedules in lunchtime - at least 01:30.
/// </summary>
public interface IConstraintTeacherLunch : IConstraint;
