namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class GeneratedTimetableMapper
{
    public static Core.Domain.GeneratedTimetable ToCoreDomainEntity(Protobuf.GeneratedTimetable protobufDto)
    {
        var coreDomainEntity = new Core.Domain.GeneratedTimetable(
            TimetableGridMapper.ToCoreDomainEntity(protobufDto.Timetable),
            protobufDto.Score
        );

        return coreDomainEntity;
    }

    public static Protobuf.GeneratedTimetable ToProtobufDto(Core.Domain.GeneratedTimetable coreDomainEntity)
    {
        var protobufDto = new Protobuf.GeneratedTimetable
        {
            Timetable = TimetableGridMapper.ToProtobuf(coreDomainEntity.Timetable),
            Score = coreDomainEntity.Score
        };

        return protobufDto;
    }
}