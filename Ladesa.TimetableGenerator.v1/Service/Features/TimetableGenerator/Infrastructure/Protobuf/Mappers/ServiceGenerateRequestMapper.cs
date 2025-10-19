using Ladesa.TimetableGenerator.v1.Protobuf.Mappers;
using Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Application.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Features.TimetableGenerator.Infrastructure.Protobuf.Mappers;

public static class ServiceGenerateRequestMapper
{
    public static ServiceGenerateRequestDto ToServiceTimetableGeneratorDto(
        Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateRequestDto dto)
    {
        return new ServiceGenerateRequestDto(
            Guid.Parse(dto.RequestId),
            GenerateRequestMapper.ToCoreApplicationDto(dto.GenerateRequest)
        );
    }

    public static Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateRequestDto ToProtobuf(
        ServiceGenerateRequestDto domain)
    {
        var dto = new Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateRequestDto
        {
            RequestId = domain.RequestId.ToString(),
            GenerateRequest = GenerateRequestMapper.ToProtobufDto(domain.GenerateRequest)
        };

        return dto;
    }
}