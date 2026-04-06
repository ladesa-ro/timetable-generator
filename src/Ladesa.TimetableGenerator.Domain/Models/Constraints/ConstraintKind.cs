namespace Ladesa.TimetableGenerator.Domain.Models.Constraints;

public enum ConstraintKind
{
    GroupOneScheduleAtSameTime,
    TeacherOneScheduleAtSameTime,
    DiaryLimitSchedulesInOneWeek,
    DiaryLimitRemaining,
    TeacherLunch,
    GroupLunch,
    TeacherNoOppositeTurns,
    Teacher12Hours,
    GroupNoOverlappingTimeSlots,
    TeacherNoOverlappingTimeSlots,
}
