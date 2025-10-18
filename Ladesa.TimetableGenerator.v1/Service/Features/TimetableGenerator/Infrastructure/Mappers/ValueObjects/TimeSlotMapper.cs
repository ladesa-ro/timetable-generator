using Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;
using Ladesa.TimetableGenerator.v1.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.ValueObjects;

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