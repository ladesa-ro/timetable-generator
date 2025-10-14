using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperDatas
{
    public static IEnumerable<DateOnly> Datas(GeradorPayload payload)
    {
        for (var data = payload.DataInicial; data <= payload.DataFinal; data = data.AddDays(1)) yield return data;
    }
}