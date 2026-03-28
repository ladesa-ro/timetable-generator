using Ladesa.TimetableGenerator.Domain.Models.Subject;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

public static class SubjectMapper
{
    public static Subject ToCoreDomainEntity(Msg.Subject messagesDto)
    {
        var coreDomainEntity = new Subject(
            messagesDto.Id, messagesDto.Name);

        return coreDomainEntity;
    }

    public static Msg.Subject ToMessagesDto(Subject domain)
    {
        var messagesDto = new Msg.Subject { Id = domain.Id, Name = domain.Name };
        return messagesDto;
    }
}
