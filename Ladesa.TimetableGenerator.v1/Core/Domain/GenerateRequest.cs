namespace Ladesa.TimetableGenerator.v1.Core.Domain;

public record GenerateRequest(
    //
    DateOnly DateStart,
    DateOnly DateEnd,
    //
    Group[] Groups,
    Teacher[] Teachers,
    Diary[] Diaries,
    //
    TimeSlot[] TimeSlots,
    //
    TimetableGrid? PreviousTimetableGrid = null,
    int BoostSameDayOfWeekAndTimeSlot = 100,
    int BoostSameDayOfWeekOnly = 50,
    int BoostSameTimeSlotOnly = 50,
    int BoostLesserDistanceFromDayOfWeek = 40,
    int BoostLesserDistanceFromTimeSlot = 40
);