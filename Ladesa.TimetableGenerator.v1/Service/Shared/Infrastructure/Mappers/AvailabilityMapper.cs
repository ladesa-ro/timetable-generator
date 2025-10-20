namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class AvailabilityMapper
{
    public static Core.Domain.Availability ToCoreDomainEntity(Protobuf.Availability protobufDto)
    {
        var coreDomainEntity = new Core.Domain.Availability(
            protobufDto.RulesUnavailability.Select(AvailabilityRuleUnavailabilityMapper.ToCoreDomainEntity).ToArray()
        );

        return coreDomainEntity;
    }

    public static Protobuf.Availability ToProtobufDto(Core.Domain.Availability coreDomainEntity)
    {
        var protobufDto = new Protobuf.Availability { };

        protobufDto.RulesUnavailability.AddRange(coreDomainEntity.RulesUnavailability
            ?.Select(AvailabilityRuleUnavailabilityMapper.ToProtobufDto).ToArray());

        return protobufDto;
    }
}