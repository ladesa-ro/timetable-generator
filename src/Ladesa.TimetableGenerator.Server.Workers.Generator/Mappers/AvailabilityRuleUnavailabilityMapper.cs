using System.Globalization;
using Ladesa.TimetableGenerator.Domain.Models.Availability;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class AvailabilityRuleUnavailabilityMapper
{
    public static AvailabilityRuleUnavailability ToCoreDomainEntity(
        Msg.RuleElement messagesDto)
    {
        var coreDomainEntity = new AvailabilityRuleUnavailability(
            messagesDto.RRule,
            messagesDto.DateStart.DateTime,
            messagesDto.DateEnd is not null ? DateTime.Parse(messagesDto.DateEnd, CultureInfo.InvariantCulture) : null
        );

        return coreDomainEntity;
    }

    public static Msg.RuleElement ToMessagesDto(
        AvailabilityRuleUnavailability coreDomainEntity)
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
