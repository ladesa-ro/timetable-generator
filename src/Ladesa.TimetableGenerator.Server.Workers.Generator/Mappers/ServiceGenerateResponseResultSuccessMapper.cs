using Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class ServiceGenerateResponseResultSuccessMapper
{
    public static ServiceGenerateResponseResultSuccessDto ToServiceDto(Msg.Result messagesDto)
    {
        var serviceDto = new ServiceGenerateResponseResultSuccessDto(
            Guid.Parse(messagesDto.RequestId),
            GenerateRequestMapper.ToCoreDomainEntity(messagesDto.GenerateRequest),
            messagesDto.GeneratedTimetables?.Select(GeneratedTimetableMapper.ToCoreDomainEntity).ToArray() ?? []
        );

        return serviceDto;
    }

    public static Msg.Result ToMessagesDto(ServiceGenerateResponseResultSuccessDto serviceDto)
    {
        var messagesDto = new Msg.Result
        {
            RequestId = serviceDto.RequestId.ToString(),
            GenerateRequest = GenerateRequestMapper.ToMessagesDto(serviceDto.GenerateTimetableCommand),
            GeneratedTimetables = serviceDto.GeneratedTimetables.Select(GeneratedTimetableMapper.ToMessagesDto).ToArray()
        };

        return messagesDto;
    }
}
