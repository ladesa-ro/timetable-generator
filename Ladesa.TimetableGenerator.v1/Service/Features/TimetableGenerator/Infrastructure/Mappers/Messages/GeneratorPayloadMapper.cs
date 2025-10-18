using Ladesa.TimetableGenerator.v1.Core.Application.DTOs;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.Entities;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.ValueObjects;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Protos;
using Ladesa.TimetableGenerator.v1.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.Messages;

public static class GeneratorPayloadMapper
{
    public static GenerateRequest ToDomain(GeneratorPayloadDto dto)
    {
        return new GenerateRequest(
            Guid.Parse(dto.RequestId),
            DateOnly.Parse(dto.DateStart),
            DateOnly.Parse(dto.DateEnd),
            dto.Groups.Select(GroupMapper.ToDomain).ToArray(),
            dto.Teachers.Select(TeacherMapper.ToDomain).ToArray(),
            dto.Diarys.Select(DiaryMapper.ToDomain).ToArray(),
            dto.TimeSlots.Select(TimeSlotMapper.ToDomain).ToArray()
        );
    }

    public static GeneratorPayloadDto ToDto(GenerateRequest domain)
    {
        var dto = new GeneratorPayloadDto
        {
            RequestId = domain.RequestId.ToString(),
            DateStart = domain.DateStart.ToString(),
            DateEnd = domain.DateEnd.ToString()
        };

        dto.Groups.AddRange(domain.Groups.Select(GroupMapper.ToDto).ToArray());
        dto.Teachers.AddRange(domain.Teachers.Select(TeacherMapper.ToDto).ToArray());
        dto.Diarys.AddRange(domain.Diaries.Select(DiaryMapper.ToDto).ToArray());
        dto.TimeSlots.AddRange(domain.TimeSlots.Select(TimeSlotMapper.ToDto).ToArray());

        return dto;
    }
}