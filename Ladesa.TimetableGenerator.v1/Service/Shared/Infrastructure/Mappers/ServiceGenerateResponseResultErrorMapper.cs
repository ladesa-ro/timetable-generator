using Ladesa.TimetableGenerator.v1.Service.Generator.Application.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Infrastructure.Mappers;

public static class ServiceGenerateResponseResultErrorMapper
{
    public static ServiceGenerateResponseResultErrorDto ToServiceDto(
        Protobuf.ServiceGenerateResponseResultError protobufDto)
    {
        var serviceDto = new ServiceGenerateResponseResultErrorDto(
            protobufDto.ErrorCode,
            protobufDto.ErrorMessage,
            protobufDto.AdditionalInfo
        );
        return serviceDto;
    }

    public static Protobuf.ServiceGenerateResponseResultError ToProtobufDto(
        ServiceGenerateResponseResultErrorDto serviceDto)
    {
        var protobufDto = new Protobuf.ServiceGenerateResponseResultError
        {
            ErrorCode = serviceDto.ErrorCode,
            ErrorMessage = serviceDto.ErrorMessage,
            AdditionalInfo = serviceDto.AdditionalInfo
        };

        return protobufDto;
    }
}