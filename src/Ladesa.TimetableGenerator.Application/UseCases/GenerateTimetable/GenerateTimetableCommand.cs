using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Domain.Models.Diary;
using Ladesa.TimetableGenerator.Domain.Models.Group;
using Ladesa.TimetableGenerator.Domain.Models.Teacher;
using Ladesa.TimetableGenerator.Domain.Models.TimeSlot;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;

public class GenerateTimetableCommand
{
    public required DateOnly DateStart { get; set; }
    public required DateOnly DateEnd { get; set; }
    public required Group[] Groups { get; set; }
    public required Teacher[] Teachers { get; set; }
    public required Diary[] Diaries { get; set; }
    public required TimeSlot[] TimeSlots { get; set; }
    public TimetableGrid? PreviousTimetableGrid { get; set; } = null;
    public int BoostSameDayOfWeekAndTimeSlot { get; set; } = 100;
    public int BoostSameDayOfWeekOnly { get; set; } = 50;
    public int BoostSameTimeSlotOnly { get; set; } = 50;
    public int BoostLesserDistanceFromDayOfWeek { get; set; } = 40;
    public int BoostLesserDistanceFromTimeSlot { get; set; } = 40;
    public ConstraintKind[]? EnabledConstraints { get; set; } = null;
}
