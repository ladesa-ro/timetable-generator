using Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class ServiceGenerateRequestMapper
{
    public static ServiceGenerateRequestDto ToServiceDto(Msg.GenerateRequest messagesDto)
    {
        var serviceDto = new ServiceGenerateRequestDto(
            RequestId: Guid.Parse(input: messagesDto.RequestId),
            GenerateTimetableCommand: GenerateRequestMapper.ToCoreDomainEntity(dto: messagesDto)
        );

        return serviceDto;
    }

    public static Msg.GenerateRequest ToMessagesDto(ServiceGenerateRequestDto serviceDto)
    {
        var messagesDto = new Msg.GenerateRequest
        {
            RequestId = serviceDto.RequestId.ToString(),
            DateStart = new DateTimeOffset(serviceDto.GenerateTimetableCommand.DateStart.ToDateTime(TimeOnly.MinValue)),
            DateEnd = new DateTimeOffset(serviceDto.GenerateTimetableCommand.DateEnd.ToDateTime(TimeOnly.MinValue)),
            Groups = serviceDto.GenerateTimetableCommand.Groups.Select(GroupMapper.ToMessagesDto).ToArray(),
            Teachers = serviceDto.GenerateTimetableCommand.Teachers.Select(TeacherMapper.ToMessagesDto).ToArray(),
            Diaries = serviceDto.GenerateTimetableCommand.Diaries.Select(DiaryMapper.ToMessagesDto).ToArray(),
            TimeSlots = serviceDto.GenerateTimetableCommand.TimeSlots.Select(TimeSlotMapper.ToMessagesDto).ToArray(),
            PreviousTimetableGrid = serviceDto.GenerateTimetableCommand.PreviousTimetableGrid is not null
                ? TimetableGridMapper.ToMessagesDto(serviceDto.GenerateTimetableCommand.PreviousTimetableGrid)
                : null
        };

        return messagesDto;
    }
}
