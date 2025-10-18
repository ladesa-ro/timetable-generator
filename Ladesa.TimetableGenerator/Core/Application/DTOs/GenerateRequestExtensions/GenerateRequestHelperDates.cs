namespace Ladesa.TimetableGenerator.Core.Application.DTOs.GenerateRequestExtensions;

public static class GenerateRequestHelperDates
{
    public static IEnumerable<DateOnly> GetDates(this GenerateRequest request)
    {
        for (var date = request.DateStart; date <= request.DateEnd; date = date.AddDays(1))
            yield return date;
    }
}