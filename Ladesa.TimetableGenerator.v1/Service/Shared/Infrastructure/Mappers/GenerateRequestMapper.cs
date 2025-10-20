using System.Globalization;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class GenerateRequestMapper
{
    public static Core.Domain.GenerateRequest ToCoreDomainEntity(Protobuf.GenerateRequest dto)
    {
        var coreDomainEntity = new Core.Domain.GenerateRequest(
            DateOnly.Parse(dto.DateStart, CultureInfo.InvariantCulture),
            DateOnly.Parse(dto.DateEnd, CultureInfo.InvariantCulture),
            dto.Groups.Select(GroupMapper.ToCoreDomainEntity).ToArray(),
            dto.Teachers.Select(TeacherMapper.ToCoreDomainEntity).ToArray(),
            dto.Diarys.Select(DiaryMapper.ToCoreDomainEntity).ToArray(),
            dto.TimeSlots.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray(),
            dto.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToCoreDomainEntity(dto.PreviousTimetableGrid)
                : null
        );
        return coreDomainEntity;
    }

    public static Protobuf.GenerateRequest ToProtobufDto(Core.Domain.GenerateRequest coreDomainEntity)
    {
        var dto = new Protobuf.GenerateRequest
        {
            DateStart = coreDomainEntity.DateStart.ToString(CultureInfo.InvariantCulture),
            DateEnd = coreDomainEntity.DateEnd.ToString(CultureInfo.InvariantCulture)
        };

        dto.Groups.AddRange(coreDomainEntity.Groups.Select(GroupMapper.ToDto).ToArray());
        dto.Teachers.AddRange(coreDomainEntity.Teachers.Select(TeacherMapper.ToProtobufDto).ToArray());
        dto.Diarys.AddRange(coreDomainEntity.Diaries.Select(DiaryMapper.ToProtobufDto).ToArray());
        dto.TimeSlots.AddRange(coreDomainEntity.TimeSlots.Select(TimeSlotMapper.ToProtobufDto).ToArray());

        dto.PreviousTimetableGrid = coreDomainEntity.PreviousTimetableGrid is not null
            ? TimetableGridMapper.ToProtobuf(coreDomainEntity.PreviousTimetableGrid)
            : null;

        return dto;
    }
}