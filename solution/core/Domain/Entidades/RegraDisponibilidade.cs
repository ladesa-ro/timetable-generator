namespace Ladesa.TimetableGenerator.Core.Domain;

public interface IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(DateOnly dataVerificacao, IntervaloDeTempo intervaloVerificacao);
}

public record RegraDisponibilidade(IRegraDisponibilidade[] Disponibilidades) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(DateOnly dataVerificacao, IntervaloDeTempo intervaloVerificacao)
    {
        return this.Disponibilidades.All(indisponibilidade =>
        {
            return indisponibilidade.VerificarDisponibilidade(dataVerificacao, intervaloVerificacao);
        });
    }
}

public record RegraIndisponibilidadeDiaDaSemana(DayOfWeek DiaDaSemana, IntervaloDeTempo Intervalo) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(DateOnly dataVerificacao, IntervaloDeTempo intervaloVerificacao)
    {
        if (DiaDaSemana == dataVerificacao.DayOfWeek)
        {
            return IntervaloDeTempo.VerificarIntervalo(Intervalo, intervaloVerificacao);
        }

        return true;
    }
}

public record RegraIndisponibilidadeDiasDaSemana(DayOfWeek[] DiasDaSemana, IntervaloDeTempo Intervalo) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(DateOnly dataVerificacao, IntervaloDeTempo intervaloVerificacao)
    {
        return DiasDaSemana.Contains(dataVerificacao.DayOfWeek)
            && IntervaloDeTempo.VerificarIntervalo(Intervalo, intervaloVerificacao);
    }
}

public record RegraIndisponibilidadeHorario(IntervaloDeTempo Intervalo) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(DateOnly dataVerificacao, IntervaloDeTempo intervaloVerificacao)
    {
        return IntervaloDeTempo.VerificarIntervalo(Intervalo, intervaloVerificacao);
    }
}

public record RegraIndisponibilidadeDataEspecifica(DateOnly Data, IntervaloDeTempo Intervalo) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(DateOnly dataVerificacao, IntervaloDeTempo intervaloVerificacao)
    {
        if (dataVerificacao == Data)
        {
            return IntervaloDeTempo.VerificarIntervalo(Intervalo, intervaloVerificacao);
        }

        return true;
    }
}

public record RegraIndisponibilidadePeriodoDatas(DateOnly DataInicio, DateOnly DataFim, IntervaloDeTempo Intervalo) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(DateOnly dataVerificacao, IntervaloDeTempo intervaloVerificacao)
    {
        if (dataVerificacao >= DataInicio && dataVerificacao <= DataFim)
        {
            return IntervaloDeTempo.VerificarIntervalo(Intervalo, intervaloVerificacao);
        }

        return true;
    }
}

public record RegraIndisponibilidadeDiaDoMes(int DiaDoMes, IntervaloDeTempo Intervalo) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(DateOnly dataVerificacao, IntervaloDeTempo intervaloVerificacao)
    {
        if (dataVerificacao.Day == DiaDoMes)
        {
            return IntervaloDeTempo.VerificarIntervalo(Intervalo, intervaloVerificacao);
        }

        return true;
    }
}

public record RegraIndisponibilidadeMesesDoAno(int[] Meses, IntervaloDeTempo Intervalo) : IRegraDisponibilidade
{
    public bool VerificarDisponibilidade(DateOnly dataVerificacao, IntervaloDeTempo intervaloVerificacao)
    {
        if (Meses.Contains(dataVerificacao.Month))
        {
            return IntervaloDeTempo.VerificarIntervalo(Intervalo, intervaloVerificacao);
        }

        return true;
    }
}
