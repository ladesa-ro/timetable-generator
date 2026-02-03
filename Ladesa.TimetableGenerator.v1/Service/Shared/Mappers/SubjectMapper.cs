namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public static class SubjectMapper
{
    public static Core.Domain.Subject ToCoreDomainEntity(Msg.Subject messagesDto)
    {
        var coreDomainEntity = new Core.Domain.Subject(
            messagesDto.Id, messagesDto.Name);

        return coreDomainEntity;
    }

    public static Msg.Subject ToMessagesDto(Core.Domain.Subject domain)
    {
        var messagesDto = new Msg.Subject { Id = domain.Id, Name = domain.Name };
        return messagesDto;
    }
}
