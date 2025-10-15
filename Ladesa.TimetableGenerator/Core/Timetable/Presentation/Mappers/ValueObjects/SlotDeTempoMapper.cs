using System.Globalization;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;
using Ladesa.TimetableGenerator.Core.Timetable.Presentation.DTOs;

namespace Ladesa.TimetableGenerator.Core.Timetable.Presentation.Mappers;

public static class SlotDeTempoMapper
{
    public static SlotDeTempo ToDomain(SlotDeTempoDto dto)
    {
        return new SlotDeTempo(dto.Inicio, dto.Fim);
    }

    public static SlotDeTempoDto ToDto(SlotDeTempo domain)
    {
        return new SlotDeTempoDto()
        {
            Inicio = domain.HorarioInicio,
            Fim = domain.HorarioFim,
        };
    }
}
