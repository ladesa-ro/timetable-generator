using System.Globalization;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class AvailabilityRuleUnavailabilityMapper
{
    public static Core.Domain.AvailabilityRuleUnavailability ToCoreDomainEntity(
        Msg.RuleElement messagesDto)
    {
        var coreDomainEntity = new Core.Domain.AvailabilityRuleUnavailability(
            messagesDto.RRule,
            messagesDto.DateStart.DateTime,
            messagesDto.DateEnd is not null ? DateTime.Parse(messagesDto.DateEnd, CultureInfo.InvariantCulture) : null
        );

        return coreDomainEntity;
    }

    public static Msg.RuleElement ToMessagesDto(
        Core.Domain.AvailabilityRuleUnavailability coreDomainEntity)
    {
        var messagesDto = new Msg.RuleElement
        {
            RRule = coreDomainEntity.RRule,
            DateStart = new DateTimeOffset(coreDomainEntity.DateStart),
            DateEnd = coreDomainEntity.DateEnd?.ToString(CultureInfo.InvariantCulture)
        };

        return messagesDto;
    }
}
