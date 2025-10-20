namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class DiaryMapper
{
    public static Core.Domain.Diary ToCoreDomainEntity(Protobuf.Diary protobufDto)
    {
        var coreDomainEntity = new Core.Domain.Diary(
            protobufDto.Id,
            protobufDto.GroupId,
            protobufDto.TeacherId,
            protobufDto.SubjectId,
            protobufDto.WeekLimit,
            protobufDto.Remaining
        );

        return coreDomainEntity;
    }

    public static Protobuf.Diary ToProtobufDto(Core.Domain.Diary coreDomainEntity)
    {
        var protobufDto = new Protobuf.Diary
        {
            Id = coreDomainEntity.Id,
            GroupId = coreDomainEntity.GroupId,
            TeacherId = coreDomainEntity.TeacherId,
            SubjectId = coreDomainEntity.SubjectId,
            WeekLimit = coreDomainEntity.WeekLimit,
            Remaining = coreDomainEntity.Remaining
        };

        return protobufDto;
    }
}