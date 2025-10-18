using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Application.Features.Generator.Core;

public record GenerationScheduleCombination(
    DateOnly Date,
    TimeSlot TimeSlot,
    string GroupId,
    string DiaryId,
    string TeacherId
);