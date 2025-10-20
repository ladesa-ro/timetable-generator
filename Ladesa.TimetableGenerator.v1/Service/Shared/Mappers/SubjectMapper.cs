namespace Ladesa.TimetableGenerator.v1.Service.Shared.Mappers;

public class SubjectMapper
{
    public static Core.Domain.Subject ToCoreDomainEntity(Protobuf.Subject protobufDto)
    {
        var coreDomainEntity = new Core.Domain.Subject(
            protobufDto.Id, protobufDto.Name);

        return coreDomainEntity;
    }

    public static Protobuf.Subject ToProtobufDto(Core.Domain.Subject domain)
    {
        var protobufDto = new Protobuf.Subject { Id = domain.Id, Name = domain.Name };
        return protobufDto;
    }
}