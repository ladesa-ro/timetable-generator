namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class TimetableGridMapper
{
    public static Core.Domain.Entities.TimetableGrid ToCoreDomainEntity(TimetableGrid dto)
    {
        return new Core.Domain.Entities.TimetableGrid(
            DateOnly.Parse(dto.DateEnd),
            DateOnly.Parse(dto.DateEnd),
            dto.TimeSlots.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray(),
            dto.Schedules.Select(TimetableGridScheduleMapper.ToCoreDomainEntity).ToArray()
        );
    }

    public static TimetableGrid ToProtobuf(Core.Domain.Entities.TimetableGrid domain)
    {
        var dto = new TimetableGrid
        {
            DateStart = domain.DateStart.ToString(),
            DateEnd = domain.DateEnd.ToString()
        };

        dto.TimeSlots.AddRange(domain.TimeSlots.Select(TimeSlotMapper.ToProtobuf).ToArray());
        dto.Schedules.AddRange(domain.Schedules.Select(TimetableGridScheduleMapper.ToProtobuf).ToArray());

        return dto;
    }
}