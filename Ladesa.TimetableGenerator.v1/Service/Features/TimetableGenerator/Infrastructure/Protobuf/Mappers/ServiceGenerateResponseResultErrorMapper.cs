using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Protobuf.Mappers;

public static class ServiceGenerateResponseResultErrorMapper
{
    public static ServiceGenerateResponseResultErrorDto ToServiceTimetableGeneratorDto(
        Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultErrorDto protobufDto)
    {
        return new ServiceGenerateResponseResultErrorDto(
            protobufDto.ErrorCode,
            protobufDto.ErrorMessage,
            protobufDto.AdditionalInfo
        );
    }

    public static Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultErrorDto ToProtobuf(
        ServiceGenerateResponseResultErrorDto applicationDto)
    {
        var dto = new Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultErrorDto
        {
            ErrorCode = applicationDto.ErrorCode,
            ErrorMessage = applicationDto.ErrorMessage,
            AdditionalInfo = applicationDto.AdditionalInfo
        };

        return dto;
    }
}