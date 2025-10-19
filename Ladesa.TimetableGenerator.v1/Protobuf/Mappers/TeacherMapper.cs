namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class TeacherMapper
{
    public static Core.Domain.Entities.Teacher ToCoreDomainEntity(Teacher dto)
    {
        return new Core.Domain.Entities.Teacher(dto.Id,
            AvailabilityMapper.ToCoreDomainValueObject(dto.AvailabilityRule));
    }

    public static Teacher ToProtobuf(Core.Domain.Entities.Teacher domain)
    {
        return new Teacher
        {
            Id = domain.Id,
            AvailabilityRule = AvailabilityMapper.ToProtobuf(domain.AvailabilityRule)
        };
    }
}