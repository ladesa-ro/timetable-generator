using System.Globalization;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class TimetableGridMapper
{
    public static Core.Domain.TimetableGrid ToCoreDomainEntity(Protobuf.TimetableGrid protobufDto)
    {
        var coreDomainEntity = new Core.Domain.TimetableGrid(
            DateOnly.Parse(protobufDto.DateEnd, CultureInfo.InvariantCulture),
            DateOnly.Parse(protobufDto.DateEnd, CultureInfo.InvariantCulture),
            protobufDto.TimeSlots.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray(),
            protobufDto.Schedules.Select(TimetableGridScheduleMapper.ToCoreDomainEntity).ToArray()
        );

        return coreDomainEntity;
    }

    public static Protobuf.TimetableGrid ToProtobuf(Core.Domain.TimetableGrid domain)
    {
        var dto = new Protobuf.TimetableGrid
        {
            DateStart = domain.DateStart.ToString(CultureInfo.InvariantCulture),
            DateEnd = domain.DateEnd.ToString(CultureInfo.InvariantCulture)
        };

        dto.TimeSlots.AddRange(domain.TimeSlots.Select(TimeSlotMapper.ToProtobufDto).ToArray());
        dto.Schedules.AddRange(domain.Schedules.Select(TimetableGridScheduleMapper.ToProtobuf).ToArray());

        return dto;
    }
}