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
    TimetableGrid? PreviousTimetableGrid = null
);