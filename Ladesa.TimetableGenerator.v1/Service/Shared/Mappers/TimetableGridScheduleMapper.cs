using System.Globalization;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class TimetableGridScheduleMapper
{
    public static Core.Domain.TimetableGridSchedule ToCoreDomainEntity(Protobuf.TimetableGridSchedule dto)
    {
        return new Core.Domain.TimetableGridSchedule(
            dto.GroupId,
            dto.DiaryId,
            dto.TeacherId,
            DateOnly.Parse(dto.Date, CultureInfo.InvariantCulture),
            TimeSlotMapper.ToCoreDomainValueObject(dto.TimeSlot)
        );
    }

    public static Protobuf.TimetableGridSchedule ToProtobuf(Core.Domain.TimetableGridSchedule domain)
    {
        return new Protobuf.TimetableGridSchedule
        {
            GroupId = domain.GroupId,
            DiaryId = domain.DiaryId,
            TeacherId = domain.TeacherId,
            Date = domain.Date.ToString(CultureInfo.InvariantCulture),
            TimeSlot = TimeSlotMapper.ToProtobufDto(domain.TimeSlot)
        };
    }
}