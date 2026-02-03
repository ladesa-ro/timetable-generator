using Ladesa.TimetableGenerator.v1.Service.Features.Generator.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class ServiceGenerateResponseMapper
{
    public static ServiceGenerateResponseDto ToServiceDto(Msg.ServiceGenerateResponse messagesDto)
    {
        var isSuccessful = string.IsNullOrEmpty(messagesDto.Result?.ErrorCode);

        var serviceDto = new ServiceGenerateResponseDto(
            RequestId: Guid.Parse(input: messagesDto.RequestId),
            IsSuccessful: isSuccessful,

            Success: isSuccessful && messagesDto.Result is not null
                ? ServiceGenerateResponseResultSuccessMapper.ToServiceDto(messagesDto: messagesDto.Result)
                : null,

            Error: !isSuccessful && messagesDto.Result is not null
                ? ServiceGenerateResponseResultErrorMapper.ToServiceDto(messagesDto: messagesDto.Result)
                : null,

            DateTimeIssued: DateOnly.FromDateTime(messagesDto.DateTimeIssued.DateTime)
        );
        return serviceDto;
    }

    public static Msg.ServiceGenerateResponse ToMessagesDto(ServiceGenerateResponseDto applicationDto)
    {
        Msg.Result? result = null;

        if (applicationDto.Success is not null)
        {
            result = ServiceGenerateResponseResultSuccessMapper.ToMessagesDto(serviceDto: applicationDto.Success);
        }
        else if (applicationDto.Error is not null)
        {
            result = ServiceGenerateResponseResultErrorMapper.ToMessagesDto(serviceDto: applicationDto.Error);
        }

        var messagesDto = new Msg.ServiceGenerateResponse
        {
            RequestId = applicationDto.RequestId.ToString(),
            DateTimeIssued = new DateTimeOffset(applicationDto.DateTimeIssued.ToDateTime(TimeOnly.MinValue)),
            Result = result
        };

        return messagesDto;
    }
}
