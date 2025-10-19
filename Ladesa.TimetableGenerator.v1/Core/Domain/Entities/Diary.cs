namespace Ladesa.TimetableGenerator.v1.Core.Domain.Entities;

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