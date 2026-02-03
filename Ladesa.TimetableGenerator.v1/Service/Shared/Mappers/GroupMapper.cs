namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class GroupMapper
{
    public static Core.Domain.Group ToCoreDomainEntity(Msg.GroupElement dto)
    {
        return new Core.Domain.Group(
            Id: dto.Id,
            Availability: AvailabilityMapper.ToCoreDomainEntity(messagesDto: dto.Availability)
        );
    }

    public static Msg.GroupElement ToMessagesDto(Core.Domain.Group domain)
    {
        return new Msg.GroupElement
        {
            Id = domain.Id,
            Availability = AvailabilityMapper.ToMessagesDto(coreDomainEntity: domain.Availability)
        };
    }
}
