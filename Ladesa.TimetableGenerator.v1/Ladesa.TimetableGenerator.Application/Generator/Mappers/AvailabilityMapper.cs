namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class AvailabilityMapper
{
    public static Domain.Models.Availability ToCoreDomainEntity(Msg.AvailabilityClass messagesDto)
    {
        var coreDomainEntity = new Domain.Models.Availability(
            messagesDto.Rules?.Select(AvailabilityRuleUnavailabilityMapper.ToCoreDomainEntity).ToArray() ?? []
        );

        return coreDomainEntity;
    }

    public static Msg.AvailabilityClass ToMessagesDto(Domain.Models.Availability coreDomainEntity)
    {
        var messagesDto = new Msg.AvailabilityClass
        {
            Rules = coreDomainEntity.RulesUnavailability
                ?.Select(AvailabilityRuleUnavailabilityMapper.ToMessagesDto).ToArray()
        };

        return messagesDto;
    }
}
