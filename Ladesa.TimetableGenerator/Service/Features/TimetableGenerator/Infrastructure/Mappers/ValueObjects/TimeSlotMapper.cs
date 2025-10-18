using Ladesa.TimetableGenerator.Core.Domain.ValueObjects;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Features.TimetableGenerator.Infrastructure.Mappers.ValueObjects;

public static class TimeSlotMapper
{
    public static TimeSlot ToDomain(TimeSlotDto dto)
    {
        return new TimeSlot(dto.Start, dto.End);
    }

    public static TimeSlotDto ToDto(TimeSlot domain)
    {
        return new TimeSlotDto { Start = domain.Start, End = domain.End };
    }
}