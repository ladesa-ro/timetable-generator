using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.v1.Core.Constraints;
using Ladesa.TimetableGenerator.v1.Core.Domain;
using LinearExpr = Google.OrTools.Sat.LinearExpr;

namespace Ladesa.TimetableGenerator.v1.Core.Generator;

public static class Generator
{
    /// <summary>
    ///     UTILITÁRIO: Gera uma lista com todas as combinações de aula possíveis
    ///     sem respeitar nenhum critério.
    /// </summary>
    private static IEnumerable<GenerationScheduleCombination> GetAllPossibleCombinations(
        GenerateRequest request
    )
    {
        var allPossibleCombinations = from data in request.GetDates()
            from timeSlot in request.TimeSlots
            from @group in request.Groups
            from diary in request.DiaryFindByGroupId(@group.Id)
            select new GenerationScheduleCombination(
                data,
                timeSlot,
                @group.Id,
                diary.Id,
                diary.TeacherId
            );

        return allPossibleCombinations;
    }

    /// <summary>
    ///     UTILITÁRIO: Gera uma lista com todas as combinações de aula possíveis,
    ///     respeitando as disponibilidades da turma e disponibilidades do professor.
    /// </summary>
    public static IEnumerable<GenerationScheduleCombination> GetAllCombinationsWithAvailability(
        GenerateRequest generateRequest
    )
    {
        var scheduleCombinations = GetAllPossibleCombinations(generateRequest);

        foreach (var scheduleCombination in scheduleCombinations)
        {
            var timeSlot = scheduleCombination.TimeSlot;

            // =====================================================================================
            var group = generateRequest.GroupFindByIdStrict(scheduleCombination.GroupId);
            var availableForGroup = group.Availability.IsAvailable(
                scheduleCombination.Date,
                timeSlot
            );
            // ===================================
            var teacher = generateRequest.TeacherFindByIdStrict(scheduleCombination.TeacherId);
            var availableForTeacher = teacher.Availability.IsAvailable(
                scheduleCombination.Date,
                timeSlot
            );
            // ===================================
            var allAvailable = availableForGroup && availableForTeacher;
            // =====================================================================================

            if (allAvailable)
                yield return scheduleCombination;
        }
    }

    /// <summary>
    ///     Ponto de partida que inicia, restringe e otimiza o modelo para
    ///     solucionar o problema da geração de horário.
    /// </summary>
    private static GenerationContext CreateContextWithRestrictionsApplied(GenerateRequest request)
    {
        var generationContext = new GenerationContext(request);

        ConstraintGroupOneScheduleAtSameTime.Apply(generationContext);
        ConstraintTeacherOneScheduleAtSameTime.Apply(generationContext);
        ConstraintDiaryLimitSchedulesInOneWeek.Apply(generationContext);
        ConstraintTeacherLunch.Apply(generationContext);
        ConstraintGroupLunch.Apply(generationContext);
        ConstraintTeacherNoOppositeTurns.Apply(generationContext);
        ConstraintTeacher12Hours.Apply(generationContext);
        // ConstraintAgruparDisciplinasParametro(contexto, "7", 2, new("13:50", "17:30"));
        // ConstraintPadronizarPRD(contexto);
        // ConstraintEspecificarPRD(contexto, "1", 5);

        OptimizeResult(generationContext);

        return generationContext;
    }

    public static IEnumerable<GeneratedTimetable> GenerateTimetables(GenerateRequest request)
    {
        var generationContext = CreateContextWithRestrictionsApplied(request);

        // ==============================================================

        var generatedTick = new AutoResetEvent(false);

        GeneratedTimetable? generatedTimetable = null;

        // thread de solução de horário para essa requisição
        var solutionGeneratorThread = new Thread(() =>
        {
            long? previousScore = null;

            do
            {
                var solver = new CpSolver { StringParameters = "enumerate_all_solutions:true" };

                var solutionPrinter = new GeneratorSolutionCallback(
                    generationContext,
                    spGeneratedTimetable =>
                    {
                        generatedTimetable = spGeneratedTimetable;
                        generatedTick.Set();
                    }
                );

                if (previousScore != null)
                    OptimizeResult(generationContext, previousScore - 1);

                var sat = solver.Solve(generationContext.CpModel, solutionPrinter);

                if (sat is CpSolverStatus.Feasible or CpSolverStatus.Optimal)
                {
                    var solverScore = solver.ObjectiveValue;
                    previousScore = (long)solverScore;
                }
                else
                {
                    previousScore = 0;
                }
            } while (previousScore > 0);

            generatedTimetable = null;
            generatedTick.Set();
        });

        solutionGeneratorThread.Start();

        do
        {
            generatedTick.WaitOne();

            if (generatedTimetable != null)
                yield return generatedTimetable;
        } while (generatedTimetable != null);
    }

    /// <summary>
    ///     Visto que podem haver várias soluções válidas possíveis, precisamos
    ///     otimizar a resposta para ser a mais satisfatório possível conforme
    ///     as preferências de agrupamento da turma e preferências
    ///     de cada professor.
    /// </summary>
    private static void OptimizeResult(
        GenerationContext contexto,
        long? limiteScore = null
    )
    {
        var qualityScore = LinearExpr.NewBuilder();

        foreach (var propostaDeAula in contexto.AllProposals)
            qualityScore.AddTerm((IntVar)propostaDeAula.ModelBoolVar, 1);

        if (limiteScore != null) contexto.CpModel.Add(qualityScore <= contexto.CpModel.NewConstant((long)limiteScore));

        contexto.CpModel.Maximize(qualityScore);

        contexto.Score = qualityScore;
    }
}