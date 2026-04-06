using Ladesa.TimetableGenerator.Domain.Models.Availability;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class AvailabilityMapper
{
    public static Availability ToCoreDomainEntity(Msg.AvailabilityClass messagesDto)
    {
        var coreDomainEntity = new Availability(
            messagesDto.Rules?.Select(AvailabilityRuleUnavailabilityMapper.ToCoreDomainEntity).ToArray() ?? []
        );

        return coreDomainEntity;
    }

    public static Msg.AvailabilityClass ToMessagesDto(Availability coreDomainEntity)
    {
        var messagesDto = new Msg.AvailabilityClass
        {
            Rules = coreDomainEntity.RulesUnavailability
                ?.Select(AvailabilityRuleUnavailabilityMapper.ToMessagesDto).ToArray()
        };

        return messagesDto;
    }
}
