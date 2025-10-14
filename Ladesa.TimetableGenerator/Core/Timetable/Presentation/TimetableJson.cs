using System.Text.Json;
using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation;

public static class TimetableJson
{
    private static readonly JsonSerializerOptions _options = CreateDefaultOptions();

    public static JsonSerializerOptions DefaultOptions => _options;

    private static JsonSerializerOptions CreateDefaultOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Polymorphic converter for IRegraDisponibilidadeDto
        options.Converters.Add(new RegraDisponibilidadeDtoConverter());

        return options;
    }

    // Generic stringify/parse helpers
    public static string Stringify<T>(T value) => JsonSerializer.Serialize(value, _options);

    public static T? Parse<T>(string json) => JsonSerializer.Deserialize<T>(json, _options);

    // Convenience shortcuts for common DTOs
    public static GeradorPayloadDto? ParseGeradorPayload(string json) => Parse<GeradorPayloadDto>(json);
    public static string StringifyHorariosGerados(IEnumerable<HorarioGeradoDto> horarios) => Stringify(horarios);
}

internal class RegraDisponibilidadeDtoConverter : JsonConverter<IRegraDisponibilidadeDto>
{
    public override IRegraDisponibilidadeDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        if (root.ValueKind != JsonValueKind.Object)
        {
            throw new JsonException("Esperado um objeto para IRegraDisponibilidadeDto.");
        }

        var tipo = DetectTipo(root);
        var raw = root.GetRawText();

        return tipo switch
        {
            "regras" => JsonSerializer.Deserialize<RegraDisponibilidadeDto>(raw, options),
            "indisponibilidade-dia-da-semana" => JsonSerializer.Deserialize<RegraIndisponibilidadeDiaDaSemanaDto>(raw, options),
            "indisponibilidade-dias-da-semana" => JsonSerializer.Deserialize<RegraIndisponibilidadeDiasDaSemanaDto>(raw, options),
            "indisponibilidade-horario" => JsonSerializer.Deserialize<RegraIndisponibilidadeHorarioDto>(raw, options),
            "indisponibilidade-data-especifica" => JsonSerializer.Deserialize<RegraIndisponibilidadeDataEspecificaDto>(raw, options),
            "indisponibilidade-periodo-datas" => JsonSerializer.Deserialize<RegraIndisponibilidadePeriodoDatasDto>(raw, options),
            "indisponibilidade-dia-do-mes" => JsonSerializer.Deserialize<RegraIndisponibilidadeDiaDoMesDto>(raw, options),
            "indisponibilidade-meses-do-ano" => JsonSerializer.Deserialize<RegraIndisponibilidadeMesesDoAnoDto>(raw, options),
            _ => throw new NotSupportedException($"Tipo de regra DTO não suportado: '{tipo}'.")
        };
    }

    public override void Write(Utf8JsonWriter writer, IRegraDisponibilidadeDto value, JsonSerializerOptions options)
    {
        // Delegate to the runtime type serializer (records already include 'tipo' as get-only property)
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }

    private static string DetectTipo(JsonElement obj)
    {
        // Prefer explicit discriminator when available
        if (TryGetString(obj, "tipo", out var tipo) && !string.IsNullOrWhiteSpace(tipo))
            return tipo!.Trim().ToLowerInvariant();

        // Intelligent fallback based on shape/fields
        if (obj.TryGetProperty("regras", out _)) return "regras";
        if (obj.TryGetProperty("dias_da_semana", out _)) return "indisponibilidade-dias-da-semana";
        if (obj.TryGetProperty("dia_da_semana", out _)) return "indisponibilidade-dia-da-semana";
        if (obj.TryGetProperty("data_inicio", out _) && obj.TryGetProperty("data_fim", out _)) return "indisponibilidade-periodo-datas";
        if (obj.TryGetProperty("data", out _)) return "indisponibilidade-data-especifica";
        if (obj.TryGetProperty("dia_do_mes", out _)) return "indisponibilidade-dia-do-mes";
        if (obj.TryGetProperty("meses", out _)) return "indisponibilidade-meses-do-ano";
        if (obj.TryGetProperty("slot", out _)) return "indisponibilidade-horario";

        throw new JsonException("Não foi possível detectar o tipo da regra de disponibilidade.");
    }

    private static bool TryGetString(JsonElement obj, string propName, out string? value)
    {
        if (obj.TryGetProperty(propName, out var el))
        {
            if (el.ValueKind == JsonValueKind.String)
            {
                value = el.GetString();
                return true;
            }
        }
        value = null;
        return false;
    }
}