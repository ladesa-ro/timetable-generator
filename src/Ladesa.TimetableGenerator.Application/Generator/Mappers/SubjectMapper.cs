namespace Ladesa.TimetableGenerator.Application.Generator.Mappers;

public static class SubjectMapper
{
    public static Domain.Models.Subject ToCoreDomainEntity(Msg.Subject messagesDto)
    {
        var coreDomainEntity = new Domain.Models.Subject(
            messagesDto.Id, messagesDto.Name);

        return coreDomainEntity;
    }

    public static Msg.Subject ToMessagesDto(Domain.Models.Subject domain)
    {
        var messagesDto = new Msg.Subject { Id = domain.Id, Name = domain.Name };
        return messagesDto;
    }
}
