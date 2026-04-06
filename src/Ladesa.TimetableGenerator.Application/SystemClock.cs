using Ladesa.TimetableGenerator.Domain.Abstractions;

namespace Ladesa.TimetableGenerator.Application;

public class SystemClock : ISystemClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
