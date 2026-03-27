namespace Ladesa.TimetableGenerator.Domain.Models.Constraints;

/// <summary>
///     CONSTRAINT: Teacher - no opposite turns on the same day.
///     Morning + night (without afternoon) is forbidden.
/// </summary>
public interface IConstraintTeacherNoOppositeTurns : IConstraint;
