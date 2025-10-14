namespace Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

public interface IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(
        DateOnly dataVerificacao,
        SlotDeTempo slotVerificacao
    );
}

public record RegraDisponibilidade(IRegraDisponibilidade[] Disponibilidades) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(
        DateOnly dataVerificacao,
        SlotDeTempo slotVerificacao
    )
    {
        return Disponibilidades.All(indisponibilidade =>
        {
            return indisponibilidade.VerificarDisponibilidade(
                dataVerificacao,
                slotVerificacao
            );
        });
    }
}

public record RegraIndisponibilidadeDiaDaSemana(DayOfWeek DiaDaSemana, SlotDeTempo Slot)
    : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(
        DateOnly dataVerificacao,
        SlotDeTempo slotVerificacao
    )
    {
        if (DiaDaSemana == dataVerificacao.DayOfWeek)
            return SlotDeTempo.VerificarIntervalo(Slot, slotVerificacao);

        return true;
    }
}

public record RegraIndisponibilidadeDiasDaSemana(
    DayOfWeek[] DiasDaSemana,
    SlotDeTempo Slot
) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(
        DateOnly dataVerificacao,
        SlotDeTempo slotVerificacao
    )
    {
        return DiasDaSemana.Contains(dataVerificacao.DayOfWeek)
               && SlotDeTempo.VerificarIntervalo(Slot, slotVerificacao);
    }
}

public record RegraIndisponibilidadeHorario(SlotDeTempo Slot) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(
        DateOnly dataVerificacao,
        SlotDeTempo slotVerificacao
    )
    {
        return SlotDeTempo.VerificarIntervalo(Slot, slotVerificacao);
    }
}

public record RegraIndisponibilidadeDataEspecifica(DateOnly Data, SlotDeTempo Slot)
    : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(
        DateOnly dataVerificacao,
        SlotDeTempo slotVerificacao
    )
    {
        if (dataVerificacao == Data)
            return SlotDeTempo.VerificarIntervalo(Slot, slotVerificacao);

        return true;
    }
}

public record RegraIndisponibilidadePeriodoDatas(
    DateOnly DataInicio,
    DateOnly DataFim,
    SlotDeTempo Slot
) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(
        DateOnly dataVerificacao,
        SlotDeTempo slotVerificacao
    )
    {
        if (dataVerificacao >= DataInicio && dataVerificacao <= DataFim)
            return SlotDeTempo.VerificarIntervalo(Slot, slotVerificacao);

        return true;
    }
}

public record RegraIndisponibilidadeDiaDoMes(int DiaDoMes, SlotDeTempo Slot)
    : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(
        DateOnly dataVerificacao,
        SlotDeTempo slotVerificacao
    )
    {
        if (dataVerificacao.Day == DiaDoMes)
            return SlotDeTempo.VerificarIntervalo(Slot, slotVerificacao);

        return true;
    }
}

public record RegraIndisponibilidadeMesesDoAno(int[] Meses, SlotDeTempo Slot)
    : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(
        DateOnly dataVerificacao,
        SlotDeTempo slotVerificacao
    )
    {
        if (Meses.Contains(dataVerificacao.Month))
            return SlotDeTempo.VerificarIntervalo(Slot, slotVerificacao);

        return true;
    }
}