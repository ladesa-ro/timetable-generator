namespace Ladesa.TimetableGenerator.Server.Workers.Generator;

/// <summary>Serializes a domain response to bytes for queue publishing.</summary>
public interface IMessageSerializer<in T>
{
    byte[] Serialize(T message);
}

/// <summary>Deserializes bytes from a queue into a domain timetableCommand.</summary>
public interface IMessageDeserializer<out T>
{
    T Deserialize(byte[] bytes);
}
