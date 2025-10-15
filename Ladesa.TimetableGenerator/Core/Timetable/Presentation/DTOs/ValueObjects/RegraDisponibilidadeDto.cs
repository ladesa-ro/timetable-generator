using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

public class JsonStringEnumMemberConverter : JsonConverter<TipoRegraDisponibilidadeDto>
{
    public override TipoRegraDisponibilidadeDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var enumText = reader.GetString();
        foreach (var field in typeof(TipoRegraDisponibilidadeDto).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var enumMember = field.GetCustomAttribute<EnumMemberAttribute>();
            if (enumMember?.Value == enumText)
                return (TipoRegraDisponibilidadeDto)field.GetValue(null);
        }
        throw new JsonException($"Valor inválido para TipoRegraDisponibilidadeDto: {enumText}");
    }

    public override void Write(Utf8JsonWriter writer, TipoRegraDisponibilidadeDto value, JsonSerializerOptions options)
    {
        var enumMember = value.GetType()
            .GetField(value.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>();

        var enumText = enumMember?.Value ?? value.ToString();
        writer.WriteStringValue(enumText);
    }
}


[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum TipoRegraDisponibilidadeDto
{
    [EnumMember(Value = "regra_disponibilidade_and")]
    RegraDisponibilidadeAnd,

    [EnumMember(Value = "regra_indisponibilidade_dia_da_semana")]
    RegraIndisponibilidadeDiaDaSemana,

    [EnumMember(Value = "regra_indisponibilidade_dias_da_semana")]
    RegraIndisponibilidadeDiasDaSemana,

    [EnumMember(Value = "regra_indisponibilidade_horario")]
    RegraIndisponibilidadeHorario,

    [EnumMember(Value = "regra_indisponibilidade_data_especifica")]
    RegraIndisponibilidadeDataEspecifica,

    [EnumMember(Value = "regra_indisponibilidade_periodo_datas")]
    RegraIndisponibilidadePeriodoDatas,

    [EnumMember(Value = "regra_indisponibilidade_dia_do_mes")]
    RegraIndisponibilidadeDiaDoMes,

    [EnumMember(Value = "regra_indisponibilidade_meses_do_ano")]
    RegraIndisponibilidadeMesesDoAno
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "tipo")]
[JsonDerivedType(typeof(RegraDisponibilidadeAndDto), "regra_disponibilidade_and")]
[JsonDerivedType(typeof(RegraIndisponibilidadeDiaDaSemanaDto), "regra_indisponibilidade_dia_da_semana")]
[JsonDerivedType(typeof(RegraIndisponibilidadeDiasDaSemanaDto), "regra_indisponibilidade_dias_da_semana")]
[JsonDerivedType(typeof(RegraIndisponibilidadeHorarioDto), "regra_indisponibilidade_horario")]
[JsonDerivedType(typeof(RegraIndisponibilidadeDataEspecificaDto), "regra_indisponibilidade_data_especifica")]
[JsonDerivedType(typeof(RegraIndisponibilidadePeriodoDatasDto), "regra_indisponibilidade_periodo_datas")]
[JsonDerivedType(typeof(RegraIndisponibilidadeDiaDoMesDto), "regra_indisponibilidade_dia_do_mes")]
[JsonDerivedType(typeof(RegraIndisponibilidadeMesesDoAnoDto), "regra_indisponibilidade_meses_do_ano")]
public interface IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    TipoRegraDisponibilidadeDto Tipo { get; }
}


public record RegraDisponibilidadeAndDto(
    [property: JsonPropertyName("regras")] IRegraDisponibilidadeDto[] Regras
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public TipoRegraDisponibilidadeDto Tipo => TipoRegraDisponibilidadeDto.RegraDisponibilidadeAnd;
}

public record RegraIndisponibilidadeDiaDaSemanaDto(
    [property: JsonPropertyName("dia_da_semana")] DayOfWeek DiaDaSemana,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public TipoRegraDisponibilidadeDto Tipo => TipoRegraDisponibilidadeDto.RegraIndisponibilidadeDiaDaSemana;
}

public record RegraIndisponibilidadeDiasDaSemanaDto(
    [property: JsonPropertyName("dias_da_semana")] DayOfWeek[] DiasDaSemana,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public TipoRegraDisponibilidadeDto Tipo => TipoRegraDisponibilidadeDto.RegraIndisponibilidadeDiasDaSemana;
}

public record RegraIndisponibilidadeHorarioDto(
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public TipoRegraDisponibilidadeDto Tipo => TipoRegraDisponibilidadeDto.RegraIndisponibilidadeHorario;
}

public record RegraIndisponibilidadeDataEspecificaDto(
    [property: JsonPropertyName("data")] DateOnly Data,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public TipoRegraDisponibilidadeDto Tipo => TipoRegraDisponibilidadeDto.RegraIndisponibilidadeDataEspecifica;
}

public record RegraIndisponibilidadePeriodoDatasDto(
    [property: JsonPropertyName("data_inicio")] DateOnly DataInicio,
    [property: JsonPropertyName("data_fim")] DateOnly DataFim,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public TipoRegraDisponibilidadeDto Tipo => TipoRegraDisponibilidadeDto.RegraIndisponibilidadePeriodoDatas;
}

public record RegraIndisponibilidadeDiaDoMesDto(
    [property: JsonPropertyName("dia_do_mes")] int DiaDoMes,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public TipoRegraDisponibilidadeDto Tipo => TipoRegraDisponibilidadeDto.RegraIndisponibilidadeDiaDoMes;
}

public record RegraIndisponibilidadeMesesDoAnoDto(
    [property: JsonPropertyName("meses")] int[] Meses,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public TipoRegraDisponibilidadeDto Tipo => TipoRegraDisponibilidadeDto.RegraIndisponibilidadeMesesDoAno;
}
