namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class GroupMapper
{
    public static Core.Domain.Group ToCoreDomainEntity(Protobuf.Group dto)
    {
        return new Core.Domain.Group(
            Id: dto.Id, 
            Availability: AvailabilityMapper.ToCoreDomainEntity(protobufDto: dto.Availability)
        );
    }

    public static Protobuf.Group ToDto(Core.Domain.Group domain)
    {
        return new Protobuf.Group
        {
            Id = domain.Id,
            Availability = AvailabilityMapper.ToProtobufDto(coreDomainEntity: domain.Availability)
        };
    }
}