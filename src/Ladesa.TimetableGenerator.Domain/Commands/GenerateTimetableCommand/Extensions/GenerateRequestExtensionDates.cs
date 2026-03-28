namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Extensions;

public static class GenerateRequestExtensionDates
{
    public static IEnumerable<DateOnly> GetDates(this GenerateTimetableCommand timetableCommand)
    {
        for (var date = timetableCommand.DateStart; date <= timetableCommand.DateEnd; date = date.AddDays(1))
            yield return date;
    }
}