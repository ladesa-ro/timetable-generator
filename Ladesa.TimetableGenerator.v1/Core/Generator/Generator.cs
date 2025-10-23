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
        ConstraintDiaryLimitRemaining.Apply(generationContext);
        ConstraintTeacherLunch.Apply(generationContext);
        ConstraintGroupLunch.Apply(generationContext);
        ConstraintTeacherNoOppositeTurns.Apply(generationContext);
        ConstraintTeacher12Hours.Apply(generationContext);
        ConstraintGroupNoOverlappingTimeSlots.Apply(generationContext);
        ConstraintTeacherNoOverlappingTimeSlots.Apply(generationContext);
        // ConstraintAgruparDisciplinasParametro(contexto, "7", 2, new("13:50", "17:30"));
        // ConstraintPadronizarPRD(contexto);
        // ConstraintEspecificarPRD(contexto, "1", 5);

        OptimizeResult(generationContext);

        return generationContext;
    }

    public static IEnumerable<GeneratedTimetable> GenerateTimetables(GenerateRequest request)
    {
        // Validate that all diaries reference existing groups and teachers
        if (request.Diaries is not null)
        {
            var groupIds = new HashSet<string>(request.Groups.Select(g => g.Id));
            var teacherIds = new HashSet<string>(request.Teachers.Select(t => t.Id));

            foreach (var diary in request.Diaries)
            {
                if (!groupIds.Contains(diary.GroupId) && !teacherIds.Contains(diary.TeacherId))
                    throw new Exception("Diary references not found: group and teacher not found.");
                if (!groupIds.Contains(diary.GroupId))
                    throw new Exception($"Group not found: {diary.GroupId}.");
                if (!teacherIds.Contains(diary.TeacherId))
                    throw new Exception($"Teacher not found: {diary.TeacherId}.");
            }
        }

        var generationContext = CreateContextWithRestrictionsApplied(request);

        // If there are no viable proposals (no dates/diaries/time slots), return a single empty timetable result
        if (generationContext.AllProposals.Count == 0)
        {
            var empty = new GeneratedTimetable(
                new TimetableGrid(request.DateStart, request.DateEnd, request.TimeSlots, Array.Empty<TimetableGridSchedule>()),
                0
            );
            yield return empty;
            yield break;
        }

        // ==============================================================

        var generatedTick = new AutoResetEvent(false);

        GeneratedTimetable? generatedTimetable = null;

        // thread de solução de horário para essa requisição
        var solutionGeneratorThread = new Thread(() =>
        {
            long? previousScore = null;
            var producedAny = false;

            do
            {
                var solver = new CpSolver { StringParameters = "enumerate_all_solutions:true" };

                var solutionPrinter = new GeneratorSolutionCallback(
                    generationContext,
                    spGeneratedTimetable =>
                    {
                        producedAny = true;
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

            if (!producedAny)
            {
                // Fallback: yield an empty timetable when the model is feasible only with zero schedules or produced nothing
                generatedTimetable = new GeneratedTimetable(
                    new TimetableGrid(request.DateStart, request.DateEnd, request.TimeSlots, Array.Empty<TimetableGridSchedule>()),
                    0
                );
                generatedTick.Set();
                // Signal completion
                generatedTimetable = null;
                generatedTick.Set();
            }
            else
            {
                generatedTimetable = null;
                generatedTick.Set();
            }
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


        // quanto mais aulas, melhor
        foreach (var propostaDeAula in contexto.AllProposals)
            qualityScore.AddTerm((IntVar)propostaDeAula.ModelBoolVar, 1);


        var generateRequest = contexto.GenerateRequest;
        var previousTimetableGrid = generateRequest.PreviousTimetableGrid;

        if (previousTimetableGrid is not null)
        {
            // bonus para aulas que caem

            foreach (var previousSchedule in previousTimetableGrid.Schedules)
            {
                var matchingProposals = (from scheduleProposal in contexto.AllProposals
                        where scheduleProposal.GroupId == previousSchedule.GroupId
                              && scheduleProposal.DiaryId == previousSchedule.DiaryId
                              && scheduleProposal.TeacherId == previousSchedule.TeacherId
                        select scheduleProposal).ToArray()
                    ;

                // same day of week
                var matchingProposalsSameDayOfWeek = (from scheduleProposal in matchingProposals
                    where
                        scheduleProposal.Date.DayOfWeek == previousSchedule.Date.DayOfWeek
                    select scheduleProposal).ToArray();

                foreach (var matchingProposalSameDay in matchingProposalsSameDayOfWeek)
                {
                    qualityScore.AddTerm((IntVar)matchingProposalSameDay.ModelBoolVar, generateRequest.BoostSameDayOfWeekOnly);
                }


                // same time slot
                var sameTimeSlotBoolVars = (from scheduleProposal in matchingProposals
                    where
                        scheduleProposal.TimeSlot == previousSchedule.TimeSlot
                    select scheduleProposal).ToArray();

                foreach (var sameTimeSlotBoolVar in sameTimeSlotBoolVars)
                {
                    qualityScore.AddTerm((IntVar)sameTimeSlotBoolVar.ModelBoolVar, generateRequest.BoostSameTimeSlotOnly);
                }
                
                // same day of week and time slot
                var sameDayAndTimeSlotBoolVars = (from scheduleProposal in matchingProposals
                    where
                        scheduleProposal.Date.DayOfWeek == previousSchedule.Date.DayOfWeek
                        && scheduleProposal.TimeSlot == previousSchedule.TimeSlot
                    select scheduleProposal).ToArray();
                
                foreach (var sameDayAndTimeSlotBoolVar in sameDayAndTimeSlotBoolVars)
                {
                    qualityScore.AddTerm((IntVar)sameDayAndTimeSlotBoolVar.ModelBoolVar, generateRequest.BoostSameDayOfWeekAndTimeSlot);
                }
                
                // in case of different day of the week, we give a bonus for the lesser distance
                var distancesByDayOfWeek = (from scheduleProposal in matchingProposals
                        group scheduleProposal by scheduleProposal.Date.DayOfWeek into g
                        select new
                        {
                            DayOfWeek = g.Key,
                            Proposals = g.AsEnumerable()
                        }).ToArray();

                foreach (var distanceByDayOfWeek in distancesByDayOfWeek)
                {
                    var distance = Math.Abs((int)distanceByDayOfWeek.DayOfWeek - (int)previousSchedule.Date.DayOfWeek);
                    var score = (7 - distance) * generateRequest.BoostLesserDistanceFromDayOfWeek;

                    foreach (var proposal in distanceByDayOfWeek.Proposals)
                    {
                        // less distance = better score
                        qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, score);
                    }
                }
                
                // in case of different time slots week, we give a bonus for the lesser distance
                var distancesByTimeSlot = (from scheduleProposal in matchingProposals
                        group scheduleProposal by scheduleProposal.TimeSlot into g
                        select new
                        {
                            TimeSlot = g.Key,
                            Proposals = g.AsEnumerable()
                        }).ToArray();
                
                foreach (var distanceByTimeSlot in distancesByTimeSlot)
                {
                    var distance = previousSchedule.TimeSlot.Distance(distanceByTimeSlot.TimeSlot);
                    var distanceAsMinutes = distance.TotalMinutes;
                    
                    // more distance = worst score
                    var score = (long)((-distanceAsMinutes) * generateRequest.BoostLesserDistanceFromTimeSlot);
                    
                    foreach (var proposal in distanceByTimeSlot.Proposals)
                    {
                        qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, score);
                    }
                }
                
            }
        }


        if (limiteScore != null) contexto.CpModel.Add(qualityScore <= contexto.CpModel.NewConstant((long)limiteScore));

        contexto.CpModel.Maximize(qualityScore);

        contexto.Score = qualityScore;
    }
}