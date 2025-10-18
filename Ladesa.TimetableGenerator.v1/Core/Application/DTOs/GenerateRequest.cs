using Ladesa.TimetableGenerator.v1.Core.Domain.Entities;
using Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.v1.Core.Application.DTOs;

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