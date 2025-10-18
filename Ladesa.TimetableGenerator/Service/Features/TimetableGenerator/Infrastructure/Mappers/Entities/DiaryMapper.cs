using Ladesa.TimetableGenerator.Core.Domain.Entities;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Features.TimetableGenerator.Infrastructure.Mappers.Entities;

public static class DiaryMapper
{
    public static Diary ToDomain(DiaryDto dto)
    {
        return new Diary(
            dto.Id,
            dto.GroupId,
            dto.TeacherId,
            dto.SubjectId,
            dto.WeekLimit,
            dto.Remaining
        );
    }

    public static DiaryDto ToDto(Diary domain)
    {
        return new DiaryDto
        {
            Id = domain.Id,
            GroupId = domain.GroupId,
            TeacherId = domain.TeacherId,
            SubjectId = domain.SubjectId,
            WeekLimit = domain.WeekLimit,
            Remaining = domain.Remaining
        };
    }
}