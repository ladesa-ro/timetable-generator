using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperDatas
{
    public static IEnumerable<DateOnly> Datas(GeneratorPayload payload)
    {
        for (var data = payload.DateStart; data <= payload.DateEnd; data = data.AddDays(1))
            yield return data;
    }
}