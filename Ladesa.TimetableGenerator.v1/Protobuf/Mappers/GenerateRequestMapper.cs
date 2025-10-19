using System.Xml;

namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class GenerateRequestMapper
{
    public static Core.Application.DTOs.GenerateRequest ToCoreApplicationDto(GenerateRequestDto dto)
    {
        return new Core.Application.DTOs.GenerateRequest(
            DateOnly.Parse(dto.DateStart),
            DateOnly.Parse(dto.DateEnd),
            dto.Groups.Select(GroupMapper.ToCoreDomainEntity).ToArray(),
            dto.Teachers.Select(TeacherMapper.ToCoreDomainEntity).ToArray(),
            dto.Diarys.Select(DiaryMapper.ToCoreDomainEntity).ToArray(),
            dto.TimeSlots.Select(TimeSlotMapper.ToCoreDomainValueObject).ToArray(),
            dto.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToCoreDomainEntity(dto.PreviousTimetableGrid)
                : null
        );
    }

    public static GenerateRequestDto ToProtobufDto(Core.Application.DTOs.GenerateRequest domain)
    {
        var dto = new GenerateRequestDto
        {
            DateStart = domain.DateStart.ToString(),
            DateEnd = domain.DateEnd.ToString()
        };

        dto.Groups.AddRange(domain.Groups.Select(GroupMapper.ToDto).ToArray());
        dto.Teachers.AddRange(domain.Teachers.Select(TeacherMapper.ToProtobuf).ToArray());
        dto.Diarys.AddRange(domain.Diaries.Select(DiaryMapper.ToProtobuf).ToArray());
        dto.TimeSlots.AddRange(domain.TimeSlots.Select(TimeSlotMapper.ToProtobuf).ToArray());

        dto.PreviousTimetableGrid = domain.PreviousTimetableGrid is not null
            ? TimetableGridMapper.ToProtobuf(domain.PreviousTimetableGrid)
            : null;

        return dto;
    }
}