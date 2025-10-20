using Ladesa.TimetableGenerator.v1.Service.Features.Generator.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class ServiceGenerateResponseResultSuccessMapper
{
    public static ServiceGenerateResponseResultSuccessDto ToServiceDto(
        Protobuf.ServiceGenerateResponseResultSuccess protobufDto)
    {
        var serviceDto = new ServiceGenerateResponseResultSuccessDto(
            Guid.Parse(protobufDto.RequestId),
            GenerateRequestMapper.ToCoreDomainEntity(protobufDto.GenerateRequest),
            protobufDto.GeneratedTimetables.Select(GeneratedTimetableMapper.ToCoreDomainEntity).ToArray()
        );

        return serviceDto;
    }

    public static Protobuf.ServiceGenerateResponseResultSuccess ToProtobufDto(
        ServiceGenerateResponseResultSuccessDto serviceDto)
    {
        var protobufDto = new Protobuf.ServiceGenerateResponseResultSuccess
        {
            RequestId = serviceDto.RequestId.ToString(),
            GenerateRequest = GenerateRequestMapper.ToProtobufDto(serviceDto.GenerateRequest)
        };

        protobufDto.GeneratedTimetables.AddRange(
            serviceDto.GeneratedTimetables.Select(GeneratedTimetableMapper.ToProtobufDto)
        );

        return protobufDto;
    }
}