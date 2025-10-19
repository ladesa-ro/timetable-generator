using Ladesa.TimetableGenerator.v1.Protobuf.Mappers;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Protobuf.Mappers;

public static class ServiceGenerateResponseMapper
{
    public static ServiceGenerateResponseDto ToServiceTimetableGeneratorDto(
        Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseDto protobufDto)
    {
        return new ServiceGenerateResponseDto(
            Guid.Parse(protobufDto.RequestId),
            protobufDto.IsSuccessful,
            protobufDto.ResultSuccess is not null
                ? ServiceGenerateResponseResultSuccessMapper.ToServiceTimetableGeneratorDto(protobufDto.ResultSuccess)
                : null,
            protobufDto.ResultError is not null
                ? ServiceGenerateResponseResultErrorMapper.ToServiceTimetableGeneratorDto(protobufDto.ResultError)
                : null,
            DateOnly.Parse(protobufDto.DateTimeIssued)
        );
    }

    public static Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseDto ToProtobufDto(
        ServiceGenerateResponseDto applicationDto)
    {
        var protobufDto = new Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseDto
        {
            RequestId = applicationDto.RequestId.ToString(),
            IsSuccessful = applicationDto.IsSuccessful,
            DateTimeIssued = applicationDto.DateTimeIssued.ToString()
        };

        if (applicationDto.Success is not null)
            protobufDto.ResultSuccess = ServiceGenerateResponseResultSuccessMapper.ToProtobuf(applicationDto.Success);

        if (applicationDto.Error is not null)
            protobufDto.ResultError = ServiceGenerateResponseResultErrorMapper.ToProtobuf(applicationDto.Error);

        return protobufDto;
    }
}