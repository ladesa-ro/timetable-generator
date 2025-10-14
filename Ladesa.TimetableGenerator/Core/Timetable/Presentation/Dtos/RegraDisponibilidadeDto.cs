using System.Text.Json.Serialization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Dtos;

public interface IRegraDisponibilidadeDto { }

public record RegraDisponibilidadeDto(
    [property: JsonPropertyName("regras")] IRegraDisponibilidadeDto[] Regras
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public string Tipo => "regras";
}

public record RegraIndisponibilidadeDiaDaSemanaDto(
    [property: JsonPropertyName("dia_da_semana")] DayOfWeek DiaDaSemana,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public string Tipo => "indisponibilidade-dia-da-semana";
}

public record RegraIndisponibilidadeDiasDaSemanaDto(
    [property: JsonPropertyName("dias_da_semana")] DayOfWeek[] DiasDaSemana,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public string Tipo => "indisponibilidade-dias-da-semana";
}

public record RegraIndisponibilidadeHorarioDto(
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public string Tipo => "indisponibilidade-horario";
}

public record RegraIndisponibilidadeDataEspecificaDto(
    [property: JsonPropertyName("data")] DateOnly Data,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public string Tipo => "indisponibilidade-data-especifica";
}

public record RegraIndisponibilidadePeriodoDatasDto(
    [property: JsonPropertyName("data_inicio")] DateOnly DataInicio,
    [property: JsonPropertyName("data_fim")] DateOnly DataFim,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public string Tipo => "indisponibilidade-periodo-datas";
}

public record RegraIndisponibilidadeDiaDoMesDto(
    [property: JsonPropertyName("dia_do_mes")] int DiaDoMes,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public string Tipo => "indisponibilidade-dia-do-mes";
}

public record RegraIndisponibilidadeMesesDoAnoDto(
    [property: JsonPropertyName("meses")] int[] Meses,
    [property: JsonPropertyName("slot")] SlotDeTempoDto Slot
) : IRegraDisponibilidadeDto
{
    [JsonPropertyName("tipo")]
    public string Tipo => "indisponibilidade-meses-do-ano";
}

public static class RegraDisponibilidadeMapper
{
    public static IRegraDisponibilidadeDto ToDto(this IRegraDisponibilidade domain)
        => domain switch
        {
            RegraDisponibilidade r => new RegraDisponibilidadeDto(r.Regras.Select(ToDto).ToArray()),
            RegraIndisponibilidadeDiaDaSemana r => new RegraIndisponibilidadeDiaDaSemanaDto(r.DiaDaSemana, r.Slot.ToDto()),
            RegraIndisponibilidadeDiasDaSemana r => new RegraIndisponibilidadeDiasDaSemanaDto(r.DiasDaSemana, r.Slot.ToDto()),
            RegraIndisponibilidadeHorario r => new RegraIndisponibilidadeHorarioDto(r.Slot.ToDto()),
            RegraIndisponibilidadeDataEspecifica r => new RegraIndisponibilidadeDataEspecificaDto(r.Data, r.Slot.ToDto()),
            RegraIndisponibilidadePeriodoDatas r => new RegraIndisponibilidadePeriodoDatasDto(r.DataInicio, r.DataFim, r.Slot.ToDto()),
            RegraIndisponibilidadeDiaDoMes r => new RegraIndisponibilidadeDiaDoMesDto(r.DiaDoMes, r.Slot.ToDto()),
            RegraIndisponibilidadeMesesDoAno r => new RegraIndisponibilidadeMesesDoAnoDto(r.Meses, r.Slot.ToDto()),
            _ => throw new NotSupportedException($"Tipo de regra não suportado: {domain.GetType().Name}")
        };

    public static IRegraDisponibilidade ToDomain(this IRegraDisponibilidadeDto dto)
        => dto switch
        {
            RegraDisponibilidadeDto r => new RegraDisponibilidade(r.Regras.Select(ToDomain).ToArray()),
            RegraIndisponibilidadeDiaDaSemanaDto r => new RegraIndisponibilidadeDiaDaSemana(r.DiaDaSemana, r.Slot.ToDomain()),
            RegraIndisponibilidadeDiasDaSemanaDto r => new RegraIndisponibilidadeDiasDaSemana(r.DiasDaSemana, r.Slot.ToDomain()),
            RegraIndisponibilidadeHorarioDto r => new RegraIndisponibilidadeHorario(r.Slot.ToDomain()),
            RegraIndisponibilidadeDataEspecificaDto r => new RegraIndisponibilidadeDataEspecifica(r.Data, r.Slot.ToDomain()),
            RegraIndisponibilidadePeriodoDatasDto r => new RegraIndisponibilidadePeriodoDatas(r.DataInicio, r.DataFim, r.Slot.ToDomain()),
            RegraIndisponibilidadeDiaDoMesDto r => new RegraIndisponibilidadeDiaDoMes(r.DiaDoMes, r.Slot.ToDomain()),
            RegraIndisponibilidadeMesesDoAnoDto r => new RegraIndisponibilidadeMesesDoAno(r.Meses, r.Slot.ToDomain()),
            _ => throw new NotSupportedException($"Tipo de regra DTO não suportado: {dto.GetType().Name}")
        };

    public static RegraDisponibilidadeDto ToDto(this RegraDisponibilidade domain)
        => (RegraDisponibilidadeDto)ToDto((IRegraDisponibilidade)domain);

    public static RegraDisponibilidade ToDomain(this RegraDisponibilidadeDto dto)
        => (RegraDisponibilidade)ToDomain((IRegraDisponibilidadeDto)dto);
}                        