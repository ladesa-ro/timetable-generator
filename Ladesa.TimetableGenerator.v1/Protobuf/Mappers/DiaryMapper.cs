namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class DiaryMapper
{
    public static Core.Domain.Entities.Diary ToCoreDomainEntity(Diary dto)
    {
        return new Core.Domain.Entities.Diary(
            dto.Id,
            dto.GroupId,
            dto.TeacherId,
            dto.SubjectId,
            dto.WeekLimit,
            dto.Remaining
        );
    }

    public static Diary ToProtobuf(Core.Domain.Entities.Diary domain)
    {
        return new Diary
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