using Ladesa.TimetableGenerator.Core.Domain.Entities;
using Ladesa.TimetableGenerator.Service.Features.TimetableGenerator.Infrastructure.Mappers.ValueObjects;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Features.TimetableGenerator.Infrastructure.Mappers.Entities;

public static class GeneratedTimetableMapper
{
    public static GeneratedTimetable ToDomain(GeneratedTimetableDto dto)
    {
        return new GeneratedTimetable(
            Guid.Parse(dto.RequestId),
            DateOnly.Parse(dto.DateStart),
            DateOnly.Parse(dto.DateEnd),
            dto.TimeSlots.Select(TimeSlotMapper.ToDomain).ToArray(),
            dto.Schedules.Select(GeneratedTimetableLessonMapper.ToDomain).ToArray(),
            dto.Score
        );
    }

    public static GeneratedTimetableDto ToDto(GeneratedTimetable domain)
    {
        var dto = new GeneratedTimetableDto
        {
            RequestId = domain.RequestId.ToString(),
            DateStart = domain.DateStart.ToString(),
            DateEnd = domain.DateEnd.ToString(),
            Score = domain.Score
        };

        dto.TimeSlots.AddRange(domain.TimeSlots.Select(TimeSlotMapper.ToDto).ToArray());
        dto.Schedules.AddRange(domain.Schedules.Select(GeneratedTimetableLessonMapper.ToDto).ToArray());

        return dto;
    }
}