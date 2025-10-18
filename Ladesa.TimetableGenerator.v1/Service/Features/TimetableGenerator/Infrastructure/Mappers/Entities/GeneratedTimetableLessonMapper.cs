using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.ValueObjects;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Protos;
using Ladesa.TimetableGenerator.v1.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Mappers.Entities;

public static class GeneratedTimetableLessonMapper
{
    public static GeneratedTimetableLesson ToDomain(GeneratedTimetableLessonDto dto)
    {
        return new GeneratedTimetableLesson(
            dto.GroupId,
            dto.DiaryId,
            dto.TeacherId,
            DateOnly.Parse(dto.Date),
            TimeSlotMapper.ToDomain(dto.TimeSlot)
        );
    }

    public static GeneratedTimetableLessonDto ToDto(GeneratedTimetableLesson domain)
    {
        return new GeneratedTimetableLessonDto
        {
            GroupId = domain.GroupId,
            DiaryId = domain.DiaryId,
            TeacherId = domain.TeacherId,
            Date = domain.Date.ToString(),
            TimeSlot = TimeSlotMapper.ToDto(domain.TimeSlot)
        };
    }
}