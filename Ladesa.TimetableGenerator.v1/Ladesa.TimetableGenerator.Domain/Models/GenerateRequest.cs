namespace Ladesa.TimetableGenerator.Domain.Models;

public record GenerateRequest
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

    public GenerateRequest(
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
        int BoostLesserDistanceFromTimeSlot = 40)
    {
        this.DateStart = DateStart;
        this.DateEnd = DateEnd;
        this.Groups = Groups ?? Array.Empty<Group>();
        this.Teachers = Teachers ?? Array.Empty<Teacher>();
        this.Diaries = Diaries ?? Array.Empty<Diary>();
        this.TimeSlots = TimeSlots ?? Array.Empty<TimeSlot>();
        this.PreviousTimetableGrid = PreviousTimetableGrid;
        this.BoostSameDayOfWeekAndTimeSlot = BoostSameDayOfWeekAndTimeSlot;
        this.BoostSameDayOfWeekOnly = BoostSameDayOfWeekOnly;
        this.BoostSameTimeSlotOnly = BoostSameTimeSlotOnly;
        this.BoostLesserDistanceFromDayOfWeek = BoostLesserDistanceFromDayOfWeek;
        this.BoostLesserDistanceFromTimeSlot = BoostLesserDistanceFromTimeSlot;

        // Validate time slots: must be strictly increasing within the day (no zero-length, no spanning midnight)
        foreach (var slot in this.TimeSlots)
        {
            var start = TimeSpan.Parse(slot.Start);
            var end = TimeSpan.Parse(slot.End);
            if (start >= end)
                throw new ArgumentException("Invalid time slot: start must be before end within the same day.");
        }

        ValidateNoDuplicates(this.Groups, g => g.Id,
            Generator.GeneratorValidationErrorCode.DuplicateGroupId, "Groups");
        ValidateNoDuplicates(this.Teachers, t => t.Id,
            Generator.GeneratorValidationErrorCode.DuplicateTeacherId, "Teachers");
        ValidateNoDuplicates(this.Diaries, d => d.Id,
            Generator.GeneratorValidationErrorCode.DuplicateDiaryId, "Diaries");
    }

    private static void ValidateNoDuplicates<T>(
        T[] items,
        Func<T, string> idSelector,
        Generator.GeneratorValidationErrorCode errorCode,
        string entityName)
    {
        if (items.GroupBy(idSelector).Any(grouped => grouped.Count() > 1))
            throw new Generator.GeneratorValidationException(errorCode, $"Duplicate entity IDs found in {entityName}.");
    }
}