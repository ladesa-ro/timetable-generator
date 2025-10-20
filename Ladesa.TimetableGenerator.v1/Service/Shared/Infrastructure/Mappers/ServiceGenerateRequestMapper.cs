using Ladesa.TimetableGenerator.v1.Service.Generator.Application.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class ServiceGenerateRequestMapper
{
    public static ServiceGenerateRequestDto ToServiceDto(
        Protobuf.ServiceGenerateRequest protobufDto)
    {
        var serviceDto = new ServiceGenerateRequestDto(
            RequestId: Guid.Parse(input: protobufDto.RequestId),
            GenerateRequest: GenerateRequestMapper.ToCoreDomainEntity(dto: protobufDto.GenerateRequest)
        );

        return serviceDto;
    }

    public static Protobuf.ServiceGenerateRequest ToProtobufDto(
        ServiceGenerateRequestDto serviceDto)
    {
        var protobufDto = new Protobuf.ServiceGenerateRequest
        {
            RequestId = serviceDto.RequestId.ToString(),
            GenerateRequest = GenerateRequestMapper.ToProtobufDto(coreDomainEntity: serviceDto.GenerateRequest)
        };

        return protobufDto;
    }
}