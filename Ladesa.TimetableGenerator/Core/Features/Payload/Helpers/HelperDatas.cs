using Ladesa.TimetableGenerator.Core.Features.Gerador.Domain;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperDatas
{
    public static IEnumerable<DateOnly> Datas(IGeradorPayload payload)
    {
        for (var data = payload.DataInicial; data <= payload.DataFinal; data = data.AddDays(1))
        {
            yield return data;
        }
    }
}
