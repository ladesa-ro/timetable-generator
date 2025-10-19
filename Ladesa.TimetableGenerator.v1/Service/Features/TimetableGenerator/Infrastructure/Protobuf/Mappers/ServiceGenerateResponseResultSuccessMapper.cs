using Ladesa.TimetableGenerator.v1.Protobuf.Mappers;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Protobuf.Mappers;

public static class ServiceGenerateResponseResultSuccessMapper
{
    public static ServiceGenerateResponseResultSuccessDto ToServiceTimetableGeneratorDto(
        Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultSuccessDto protobufDto)
    {
        return new ServiceGenerateResponseResultSuccessDto(
            Guid.Parse(protobufDto.RequestId),
            GenerateRequestMapper.ToCoreApplicationDto(protobufDto.GenerateRequest),
            protobufDto.GeneratedTimetables.Select(GeneratedTimetableMapper.ToCoreDomainEntity).ToArray()
        );
    }

    public static Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultSuccessDto ToProtobuf(
        ServiceGenerateResponseResultSuccessDto applicationDto)
    {
        var dto = new Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultSuccessDto
        {
            RequestId = applicationDto.RequestId.ToString(),
            GenerateRequest = GenerateRequestMapper.ToProtobufDto(applicationDto.GenerateRequest)
        };

        dto.GeneratedTimetables.AddRange(
            applicationDto.GeneratedTimetables.Select(GeneratedTimetableMapper.ToProtobufDto));

        return dto;
    }
}