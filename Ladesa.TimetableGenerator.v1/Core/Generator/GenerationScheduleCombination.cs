using Ladesa.TimetableGenerator.v1.Core.Domain;

namespace Ladesa.TimetableGenerator.v1.Core.Generator;

public record GenerationScheduleCombination(
    DateOnly Date,
    TimeSlot TimeSlot,
    string GroupId,
    string DiaryId,
    string TeacherId
);