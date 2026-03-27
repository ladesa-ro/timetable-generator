namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class GroupMapper
{
    public static Domain.Models.Group ToCoreDomainEntity(Msg.GroupElement dto)
    {
        return new Domain.Models.Group(
            Id: dto.Id,
            Availability: AvailabilityMapper.ToCoreDomainEntity(messagesDto: dto.Availability)
        );
    }

    public static Msg.GroupElement ToMessagesDto(Domain.Models.Group domain)
    {
        return new Msg.GroupElement
        {
            Id = domain.Id,
            Availability = AvailabilityMapper.ToMessagesDto(coreDomainEntity: domain.Availability)
        };
    }
}
