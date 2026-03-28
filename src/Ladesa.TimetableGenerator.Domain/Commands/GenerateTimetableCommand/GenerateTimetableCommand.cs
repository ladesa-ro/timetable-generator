using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Domain.Models.Diary;
using Ladesa.TimetableGenerator.Domain.Models.Group;
using Ladesa.TimetableGenerator.Domain.Models.Teacher;
using Ladesa.TimetableGenerator.Domain.Models.TimeSlot;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;

public class GenerateTimetableCommand
{
    public required DateOnly DateStart { get; set; }
    public required DateOnly DateEnd { get; set; }
    public required Group[] Groups { get; set; }
    public required Teacher[] Teachers { get; set; }
    public required Diary[] Diaries { get; set; }
    public required TimeSlot[] TimeSlots { get; set; }
    public required TimetableGrid? PreviousTimetableGrid { get; set; }
    public required int BoostSameDayOfWeekAndTimeSlot { get; set; } = 100;
    public required int BoostSameDayOfWeekOnly { get; set; } = 50;
    public required int BoostSameTimeSlotOnly { get; set; } = 50;
    public required int BoostLesserDistanceFromDayOfWeek { get; set; } = 40;
    public required int BoostLesserDistanceFromTimeSlot { get; set; } = 40;
    public required ConstraintKind[]? EnabledConstraints { get; set; } = null;
}
