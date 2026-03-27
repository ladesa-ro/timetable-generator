namespace Ladesa.TimetableGenerator.Domain.Models.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no more than one schedule at the same time.
/// </summary>
public interface IConstraintTeacherOneScheduleAtSameTime : IConstraint;
