namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class GroupMapper
{
    public static Core.Domain.Entities.Group ToCoreDomainEntity(Group dto)
    {
        return new Core.Domain.Entities.Group(dto.Id, AvailabilityMapper.ToCoreDomainValueObject(dto.AvailabilityRule));
    }

    public static Group ToDto(Core.Domain.Entities.Group domain)
    {
        return new Group
        {
            Id = domain.Id,
            AvailabilityRule = AvailabilityMapper.ToProtobuf(domain.AvailabilityRule)
        };
    }
}