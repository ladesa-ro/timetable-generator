namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public class SubjectMapper
{
    public static Core.Domain.Entities.Subject ToCoreDomainEntity(Subject dto)
    {
        return new Core.Domain.Entities.Subject(dto.Id, dto.Name);
    }

    public static Subject ToDto(Core.Domain.Entities.Subject domain)
    {
        return new Subject { Id = domain.Id, Name = domain.Name };
    }
}