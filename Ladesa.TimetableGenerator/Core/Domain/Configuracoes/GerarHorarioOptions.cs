namespace Ladesa.TimetableGenerator.Core.Domain;

public record GerarHorarioOptions(
    DateOnly DataInicial,
    DateOnly DataFinal,
    Turma[] Turmas,
    Professor[] Professores,
    IntervaloDeTempo[] HorariosDeAula,
    Diario[] Diarios
)
{
    public IEnumerable<DateOnly> Datas()
    {
        for (var data = DataInicial; data <= DataFinal; data = data.AddDays(1))
        {
            yield return data;
        }
    }

    public Professor? ProfessorFindById(string professorId)
    {
        var professor = Professores.ToList().Find(professor => professor.Id == professorId);
        return professor;
    }

    public Professor ProfessorFindByIdStrict(string professorId, string? exceptionContext = null)
    {
        var professor = ProfessorFindById(professorId);

        if (professor == null)
        {
            throw new Exception($"Professor não encontrado: {professorId}{exceptionContext}.");
        }
        ;

        return professor;
    }

    public Diario? DiarioFindById(string diarioId)
    {
        var diario = Diarios.ToList().Find(diario => diario.Id == diarioId);
        return diario;
    }

    public Diario DiarioFindByIdStrict(string diarioId, string? exceptionContext = null)
    {
        var diario = DiarioFindById(diarioId);

        if (diario == null)
        {
            throw new Exception($"Diário não encontrado: {diarioId}{exceptionContext}.");
        }
        ;

        return diario;
    }

    public Turma TurmaFindByIdStrict(string turmaId, string? exceptionContext = null)
    {
        var turma = TurmaFindById(turmaId);

        if (turma == null)
            throw new Exception($"Diário não encontrado: {turmaId}{exceptionContext}.");

        return turma;
    }

    public Turma? TurmaFindById(string turmaId)
    {
        var turma = Turmas.ToList().Find(turma => turma.Id == turmaId);
        return turma;
    }

    public IntervaloDeTempo? HorarioDeAulaByIndex(int horarioDeAulaIndex)
    {
        var horarioDeAula = HorariosDeAula[horarioDeAulaIndex];
        return horarioDeAula;
    }

    public IntervaloDeTempo HorarioDeAulaFindByIndexStrict(
        int horarioDeAulaIndex,
        string? exceptionContext = null
    )
    {
        var horarioDeAula = HorarioDeAulaByIndex(horarioDeAulaIndex);

        if (horarioDeAula == null)
            throw new Exception($"Horário de aula não encontrado: índice {horarioDeAulaIndex}.");

        return horarioDeAula;
    }

    public IEnumerable<Diario> DiariosByTurmaId(string turmaId)
    {
        return Diarios.Where(diario => diario.TurmaId == turmaId).ToList();
    }

    public bool ProfessorEstaVinculadoAoDiario(string professorId, string diarioId)
    {
        var diario = DiarioFindByIdStrict(diarioId);
        var professor = ProfessorFindByIdStrict(professorId);

        return diario.ProfessorId == professor.Id;
    }
}
