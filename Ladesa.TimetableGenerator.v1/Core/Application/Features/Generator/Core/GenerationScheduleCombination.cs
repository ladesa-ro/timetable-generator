using Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.v1.Core.Application.Features.Generator.Core;

public record GenerationScheduleCombination(
    DateOnly Date,
    TimeSlot TimeSlot,
    string GroupId,
    string DiaryId,
    string TeacherId
);