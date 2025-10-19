namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class TimetableGridScheduleMapper
{
    public static Core.Domain.Entities.TimetableGridSchedule ToCoreDomainEntity(TimetableGridSchedule dto)
    {
        return new Core.Domain.Entities.TimetableGridSchedule(
            dto.GroupId,
            dto.DiaryId,
            dto.TeacherId,
            DateOnly.Parse(dto.Date),
            TimeSlotMapper.ToCoreDomainValueObject(dto.TimeSlot)
        );
    }

    public static TimetableGridSchedule ToProtobuf(Core.Domain.Entities.TimetableGridSchedule domain)
    {
        return new TimetableGridSchedule
        {
            GroupId = domain.GroupId,
            DiaryId = domain.DiaryId,
            TeacherId = domain.TeacherId,
            Date = domain.Date.ToString(),
            TimeSlot = TimeSlotMapper.ToProtobuf(domain.TimeSlot)
        };
    }
}