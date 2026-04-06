using Ladesa.TimetableGenerator.Domain.Models.Group;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class GroupMapper
{
    public static Group ToCoreDomainEntity(Msg.GroupElement dto)
        => EntityWithAvailabilityMapper.GroupToCoreDomainEntity(dto);

    public static Msg.GroupElement ToMessagesDto(Group domain)
        => EntityWithAvailabilityMapper.GroupToMessagesDto(domain);
}
