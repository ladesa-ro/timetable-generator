namespace Ladesa.TimetableGenerator.Core.Domain;

public class HorarioGerado(
    DateOnly DataInicial,
    DateOnly DataFinal,
    IntervaloDeTempo[] HorariosDeAula,
    HorarioGeradoAula[] Aulas
);
