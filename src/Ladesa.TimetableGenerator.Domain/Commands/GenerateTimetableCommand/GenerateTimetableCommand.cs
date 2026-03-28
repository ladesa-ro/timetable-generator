using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Domain.Models.Diary;
using Ladesa.TimetableGenerator.Domain.Models.Group;
using Ladesa.TimetableGenerator.Domain.Models.Teacher;
using Ladesa.TimetableGenerator.Domain.Models.TimeSlot;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;

namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;

public record GenerateTimetableCommand
{
    public DateOnly DateStart { get; }
    public DateOnly DateEnd { get; }
    public Group[] Groups { get; }
    public Teacher[] Teachers { get; }
    public Diary[] Diaries { get; }
    public TimeSlot[] TimeSlots { get; }
    public TimetableGrid? PreviousTimetableGrid { get; }
    public int BoostSameDayOfWeekAndTimeSlot { get; }
    public int BoostSameDayOfWeekOnly { get; }
    public int BoostSameTimeSlotOnly { get; }
    public int BoostLesserDistanceFromDayOfWeek { get; }
    public int BoostLesserDistanceFromTimeSlot { get; }
    public ConstraintKind[]? EnabledConstraints { get; }

    public GenerateTimetableCommand(
        DateOnly DateStart,
        DateOnly DateEnd,
        Group[] Groups,
        Teacher[] Teachers,
        Diary[] Diaries,
        TimeSlot[] TimeSlots,
        TimetableGrid? PreviousTimetableGrid = null,
        int BoostSameDayOfWeekAndTimeSlot = 100,
        int BoostSameDayOfWeekOnly = 50,
        int BoostSameTimeSlotOnly = 50,
        int BoostLesserDistanceFromDayOfWeek = 40,
        int BoostLesserDistanceFromTimeSlot = 40,
        ConstraintKind[]? EnabledConstraints = null)
    {
        this.DateStart = DateStart;
        this.DateEnd = DateEnd;
        
        this.Groups = Groups ?? [];
        this.Teachers = Teachers ?? [];
        this.Diaries = Diaries ?? [];
        this.TimeSlots = TimeSlots ?? [];
       
        this.PreviousTimetableGrid = PreviousTimetableGrid;
        this.BoostSameDayOfWeekAndTimeSlot = BoostSameDayOfWeekAndTimeSlot;
        this.BoostSameDayOfWeekOnly = BoostSameDayOfWeekOnly;
        this.BoostSameTimeSlotOnly = BoostSameTimeSlotOnly;
        this.BoostLesserDistanceFromDayOfWeek = BoostLesserDistanceFromDayOfWeek;
        this.BoostLesserDistanceFromTimeSlot = BoostLesserDistanceFromTimeSlot;
        this.EnabledConstraints = EnabledConstraints;

        // Validate time slots: must be strictly increasing within the day (no zero-length, no spanning midnight)
        foreach (var slot in this.TimeSlots)
        {
            var start = TimeSpan.Parse(slot.Start);
            var end = TimeSpan.Parse(slot.End);
            if (start >= end)
                throw new ArgumentException("Invalid time slot: start must be before end within the same day.");
        }
        
        GeneratorValidationException.ValidateNoDuplicates(this.Groups, g => g.Id,
            GeneratorValidationErrorCode.DuplicateGroupId, "Groups");
        
        GeneratorValidationException.ValidateNoDuplicates(this.Teachers, t => t.Id,
            GeneratorValidationErrorCode.DuplicateTeacherId, "Teachers");
        
        GeneratorValidationException.ValidateNoDuplicates(this.Diaries, d => d.Id,
            GeneratorValidationErrorCode.DuplicateDiaryId, "Diaries");
    }
}