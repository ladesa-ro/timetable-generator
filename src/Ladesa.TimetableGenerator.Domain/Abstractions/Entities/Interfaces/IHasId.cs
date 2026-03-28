namespace Ladesa.TimetableGenerator.Domain.Abstractions.Entities.Interfaces;

/// <summary>
///     Marker interface for entities with a string identifier.
///     IDs are opaque strings — no specific format is enforced.
///     Consumers should treat them as unique keys within their entity type.
/// </summary>
public interface IHasId
{
    string Id { get; }
}
