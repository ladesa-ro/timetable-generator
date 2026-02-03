namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class AvailabilityMapper
{
    public static Core.Domain.Availability ToCoreDomainEntity(Msg.AvailabilityClass messagesDto)
    {
        var coreDomainEntity = new Core.Domain.Availability(
            messagesDto.Rules?.Select(AvailabilityRuleUnavailabilityMapper.ToCoreDomainEntity).ToArray() ?? []
        );

        return coreDomainEntity;
    }

    public static Msg.AvailabilityClass ToMessagesDto(Core.Domain.Availability coreDomainEntity)
    {
        var messagesDto = new Msg.AvailabilityClass
        {
            Rules = coreDomainEntity.RulesUnavailability
                ?.Select(AvailabilityRuleUnavailabilityMapper.ToMessagesDto).ToArray()
        };

        return messagesDto;
    }
}
