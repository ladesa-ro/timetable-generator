using Ladesa.TimetableGenerator.Core.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Application.DTOs;

public record GenerateRequest(
    Guid RequestId,
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
)
{
    public IEnumerable<DateOnly> GetDates()
    {
        for (var date = DateStart; date <= DateEnd; date = date.AddDays(1))
            yield return date;
    }
};