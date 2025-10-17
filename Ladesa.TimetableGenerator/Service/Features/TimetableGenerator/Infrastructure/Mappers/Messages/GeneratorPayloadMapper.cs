using Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;
using Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.Entities;
using Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.ValueObjects;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.Messages;

public static class GeneratorPayloadMapper
{
    public static GeneratorPayload ToDomain(GeneratorPayloadDto dto)
    {
        return new GeneratorPayload(
            Guid.Parse(dto.RequestId),
            DateOnly.Parse(dto.DateStart),
            DateOnly.Parse(dto.DateEnd),
            dto.Groups.Select(GroupMapper.ToDomain).ToArray(),
            dto.Teachers.Select(TeacherMapper.ToDomain).ToArray(),
            dto.Diarys.Select(DiaryMapper.ToDomain).ToArray(),
            dto.TimeSlots.Select(TimeSlotMapper.ToDomain).ToArray()
        );
    }

    public static GeneratorPayloadDto ToDto(GeneratorPayload domain)
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