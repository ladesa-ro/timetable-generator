using System.Text.Json;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.JsonSerialization;

public class BaseJsonSerializer<TDto>
{
    public static JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true
    };
    
    public static TDto ToDto(string jsonString)
    {
        var asDto = JsonSerializer.Deserialize<TDto>(jsonString);
        return asDto;
    }

    public static string ToJson(object data)
    {
        return JsonSerializer.Serialize(data, options);
    }
}
