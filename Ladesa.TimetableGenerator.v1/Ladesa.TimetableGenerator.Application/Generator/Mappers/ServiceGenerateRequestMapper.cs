using Ladesa.TimetableGenerator.Application.Generator.DTOs;

namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class ServiceGenerateRequestMapper
{
    public static ServiceGenerateRequestDto ToServiceDto(Msg.GenerateRequest messagesDto)
    {
        var serviceDto = new ServiceGenerateRequestDto(
            RequestId: Guid.Parse(input: messagesDto.RequestId),
            GenerateRequest: GenerateRequestMapper.ToCoreDomainEntity(dto: messagesDto)
        );

        return serviceDto;
    }

    public static Msg.GenerateRequest ToMessagesDto(ServiceGenerateRequestDto serviceDto)
    {
        var messagesDto = new Msg.GenerateRequest
        {
            RequestId = serviceDto.RequestId.ToString(),
            DateStart = new DateTimeOffset(serviceDto.GenerateRequest.DateStart.ToDateTime(TimeOnly.MinValue)),
            DateEnd = new DateTimeOffset(serviceDto.GenerateRequest.DateEnd.ToDateTime(TimeOnly.MinValue)),
            Groups = serviceDto.GenerateRequest.Groups.Select(GroupMapper.ToMessagesDto).ToArray(),
            Teachers = serviceDto.GenerateRequest.Teachers.Select(TeacherMapper.ToMessagesDto).ToArray(),
            Diaries = serviceDto.GenerateRequest.Diaries.Select(DiaryMapper.ToMessagesDto).ToArray(),
            TimeSlots = serviceDto.GenerateRequest.TimeSlots.Select(TimeSlotMapper.ToMessagesDto).ToArray(),
            PreviousTimetableGrid = serviceDto.GenerateRequest.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToMessagesDto(serviceDto.GenerateRequest.PreviousTimetableGrid)
                : null
        };

        return messagesDto;
    }
}
