using System.Globalization;
using Ladesa.TimetableGenerator.v1.Service.Features.Generator.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class ServiceGenerateResponseMapper
{
    public static ServiceGenerateResponseDto ToServiceDto(
        Protobuf.ServiceGenerateResponse protobufDto)
    {
        var serviceDto = new ServiceGenerateResponseDto(
            RequestId: Guid.Parse(input: protobufDto.RequestId),
            IsSuccessful: protobufDto.IsSuccessful,
            
            Success: protobufDto.ResultSuccess is not null
                ? ServiceGenerateResponseResultSuccessMapper.ToServiceDto(
                    protobufDto: protobufDto.ResultSuccess)
                : null,
            
            Error: protobufDto.ResultError is not null
                ? ServiceGenerateResponseResultErrorMapper.ToServiceDto(
                    protobufDto: protobufDto.ResultError)
                : null,
            
            DateTimeIssued: DateOnly.Parse(s: protobufDto.DateTimeIssued, provider: CultureInfo.InvariantCulture)
        );
        return serviceDto;
    }

    public static Protobuf.ServiceGenerateResponse ToProtobufDto(
        ServiceGenerateResponseDto applicationDto)
    {
        var protobufDto = new Protobuf.ServiceGenerateResponse
        {
            RequestId = applicationDto.RequestId.ToString(),
            IsSuccessful = applicationDto.IsSuccessful,
            DateTimeIssued = applicationDto.DateTimeIssued.ToString(provider: CultureInfo.InvariantCulture)
        };

        if (applicationDto.Success is not null)
            protobufDto.ResultSuccess = ServiceGenerateResponseResultSuccessMapper.ToProtobufDto(serviceDto: applicationDto.Success);

        if (applicationDto.Error is not null)
            protobufDto.ResultError = ServiceGenerateResponseResultErrorMapper.ToProtobufDto(serviceDto: applicationDto.Error);

        return protobufDto;
    }
}