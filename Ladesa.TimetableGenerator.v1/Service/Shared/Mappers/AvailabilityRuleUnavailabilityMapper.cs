using System.Globalization;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class AvailabilityRuleUnavailabilityMapper
{
    public static Core.Domain.AvailabilityRuleUnavailability ToCoreDomainEntity(
        Protobuf.AvailabilityRuleUnavailability protobufDto)
    {
        var coreDomainEntity = new Core.Domain.AvailabilityRuleUnavailability(
            protobufDto.RRule,
            DateTime.Parse(protobufDto.DateStart, CultureInfo.InvariantCulture),
            protobufDto.DateEnd is not null ? DateTime.Parse(protobufDto.DateEnd, CultureInfo.InvariantCulture) : null
        );

        return coreDomainEntity;
    }

    public static Protobuf.AvailabilityRuleUnavailability ToProtobufDto(
        Core.Domain.AvailabilityRuleUnavailability coreDomainEntity)
    {
        var protobufDto = new Protobuf.AvailabilityRuleUnavailability
        {
            RRule = coreDomainEntity.RRule,
            DateStart = coreDomainEntity.DateStart.ToString(CultureInfo.InvariantCulture),
            DateEnd = coreDomainEntity.DateEnd?.ToString(CultureInfo.InvariantCulture)
        };

        return protobufDto;
    }
}