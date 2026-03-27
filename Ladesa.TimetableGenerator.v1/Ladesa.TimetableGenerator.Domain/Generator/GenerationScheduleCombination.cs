using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Domain.Generator;

public record GenerationScheduleCombination(
    DateOnly Date,
    TimeSlot TimeSlot,
    string GroupId,
    string DiaryId,
    string TeacherId
);