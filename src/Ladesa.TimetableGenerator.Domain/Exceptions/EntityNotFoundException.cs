namespace Ladesa.TimetableGenerator.Domain.Exceptions;

public class EntityNotFoundException(string entityType, string id)
    : Exception($"{entityType} not found: {id}.")
{
    public string EntityType { get; } = entityType;
    public string EntityId { get; } = id;
}
