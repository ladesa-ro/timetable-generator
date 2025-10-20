namespace Ladesa.TimetableGenerator.v1.Core.Domain;

public record Diary(
    string Id,
    //
    string GroupId,
    string TeacherId,
    string SubjectId,
    //
    int WeekLimit,
    int Remaining
);