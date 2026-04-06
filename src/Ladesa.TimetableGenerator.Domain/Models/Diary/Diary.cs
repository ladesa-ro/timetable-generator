using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Domain.Abstractions.Entities.Interfaces;

namespace Ladesa.TimetableGenerator.Domain.Models.Diary;

public record Diary(
    string Id,
    string GroupId,
    string TeacherId,
    string SubjectId,
    int WeekLimit,
    int Remaining
) : IHasId;
