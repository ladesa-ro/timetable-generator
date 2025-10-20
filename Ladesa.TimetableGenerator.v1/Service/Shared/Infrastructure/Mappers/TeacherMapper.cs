namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class TeacherMapper
{
    public static Core.Domain.Teacher ToCoreDomainEntity(Protobuf.Teacher protobufDto)
    {
        var coreDomainEntity = new Core.Domain.Teacher(
            protobufDto.Id,
            AvailabilityMapper.ToCoreDomainEntity(protobufDto.Availability)
        );

        return coreDomainEntity;
    }

    public static Protobuf.Teacher ToProtobufDto(Core.Domain.Teacher coreDomainEntity)
    {
        return new Protobuf.Teacher
        {
            Id = coreDomainEntity.Id,
            Availability = AvailabilityMapper.ToProtobufDto(coreDomainEntity.Availability)
        };
    }
}