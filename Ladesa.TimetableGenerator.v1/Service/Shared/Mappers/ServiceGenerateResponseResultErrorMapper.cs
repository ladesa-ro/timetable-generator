using Ladesa.TimetableGenerator.v1.Service.Features.Generator.DTOs;

namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class ServiceGenerateResponseResultErrorMapper
{
    public static ServiceGenerateResponseResultErrorDto ToServiceDto(Msg.Result messagesDto)
    {
        var serviceDto = new ServiceGenerateResponseResultErrorDto(
            messagesDto.ErrorCode,
            messagesDto.ErrorMessage,
            messagesDto.AdditionalInfo
        );
        return serviceDto;
    }

    public static Msg.Result ToMessagesDto(ServiceGenerateResponseResultErrorDto serviceDto)
    {
        var messagesDto = new Msg.Result
        {
            ErrorCode = serviceDto.ErrorCode,
            ErrorMessage = serviceDto.ErrorMessage,
            AdditionalInfo = serviceDto.AdditionalInfo
        };

        return messagesDto;
    }
}
