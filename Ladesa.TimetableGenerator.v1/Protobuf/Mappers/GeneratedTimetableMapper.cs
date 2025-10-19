namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class GeneratedTimetableMapper
{
    public static Core.Domain.Entities.GeneratedTimetable ToCoreDomainEntity(GeneratedTimetable protobufTdo)
    {
        return new Core.Domain.Entities.GeneratedTimetable(
            TimetableGridMapper.ToCoreDomainEntity(protobufTdo.Timetable),
            protobufTdo.Score
        );
    }

    public static GeneratedTimetable ToProtobufDto(Core.Domain.Entities.GeneratedTimetable domainEntity)
    {
        var dto = new GeneratedTimetable
        {
            Timetable = TimetableGridMapper.ToProtobuf(domainEntity.Timetable),
            Score = domainEntity.Score
        };

        return dto;
    }
}