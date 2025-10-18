using Ladesa.TimetableGenerator.Core.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Application.DTOs;

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