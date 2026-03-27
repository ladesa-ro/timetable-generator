using System.Globalization;

namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class AvailabilityRuleUnavailabilityMapper
{
    public static Domain.Models.AvailabilityRuleUnavailability ToCoreDomainEntity(
        Msg.RuleElement messagesDto)
    {
        var coreDomainEntity = new Domain.Models.AvailabilityRuleUnavailability(
            messagesDto.RRule,
            messagesDto.DateStart.DateTime,
            messagesDto.DateEnd is not null ? DateTime.Parse(messagesDto.DateEnd, CultureInfo.InvariantCulture) : null
        );

        return coreDomainEntity;
    }

    public static Msg.RuleElement ToMessagesDto(
        Domain.Models.AvailabilityRuleUnavailability coreDomainEntity)
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
