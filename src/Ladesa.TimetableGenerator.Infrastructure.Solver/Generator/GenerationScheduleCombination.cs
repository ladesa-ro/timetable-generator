using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

public record GenerationScheduleCombination(
    DateOnly Date,
    TimeSlot TimeSlot,
    string GroupId,
    string DiaryId,
    string TeacherId
);