using System.Threading.Channels;
using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Domain.Constraints;
using Ladesa.TimetableGenerator.Domain.Models;
using LinearExpr = Google.OrTools.Sat.LinearExpr;

namespace Ladesa.TimetableGenerator.Domain.Generator;

public static class Generator
{
    /// <summary>
    ///     Generates all possible schedule combinations (date x timeslot x group x diary)
    ///     without applying any constraints.
    /// </summary>
    private static IEnumerable<GenerationScheduleCombination> GetAllPossibleCombinations(
        GenerateRequest request
    )
    {
        var allPossibleCombinations = from date in request.GetDates()
            from timeSlot in request.TimeSlots
            from grp in request.Groups
            from diary in request.DiaryFindByGroupId(grp.Id)
            select new GenerationScheduleCombination(
                date,
                timeSlot,
                grp.Id,
                diary.Id,
                diary.TeacherId
            );

        return allPossibleCombinations;
    }

    /// <summary>
    ///     Generates all possible schedule combinations, filtering by
    ///     group and teacher availability rules.
    /// </summary>
    public static IEnumerable<GenerationScheduleCombination> GetAllCombinationsWithAvailability(
        GenerateRequest generateRequest
    )
    {
        var scheduleCombinations = GetAllPossibleCombinations(generateRequest);

        foreach (var scheduleCombination in scheduleCombinations)
        {
            var timeSlot = scheduleCombination.TimeSlot;

            var group = generateRequest.GroupFindByIdStrict(scheduleCombination.GroupId);
            var availableForGroup = group.Availability.IsAvailable(scheduleCombination.Date, timeSlot);

            var teacher = generateRequest.TeacherFindByIdStrict(scheduleCombination.TeacherId);
            var availableForTeacher = teacher.Availability.IsAvailable(scheduleCombination.Date, timeSlot);

            if (availableForGroup && availableForTeacher)
                yield return scheduleCombination;
        }
    }

    /// <summary>
    ///     Creates a generation context with all constraints applied and
    ///     the objective function configured for optimization.
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

        OptimizeResult(generationContext);

        return generationContext;
    }

    /// <summary>
    ///     Generates timetable solutions for the given request, iteratively improving
    ///     quality. Yields results as they are found by the solver.
    /// </summary>
    /// <param name="request">The generation request containing groups, teachers, diaries, time slots, and constraints.</param>
    /// <returns>An enumerable of generated timetables, ordered by decreasing quality score.</returns>
    public static IEnumerable<GeneratedTimetable> GenerateTimetables(GenerateRequest request)
    {
        ValidateDiaryReferences(request);

        var generationContext = CreateContextWithRestrictionsApplied(request);

        if (generationContext.AllProposals.Count == 0)
        {
            yield return CreateEmptyTimetable(request);
            yield break;
        }

        var channel = Channel.CreateUnbounded<GeneratedTimetable>();

        var solverTask = Task.Run(() => SolveAndWriteToChannel(channel.Writer, generationContext, request));

        foreach (var timetable in ReadChannel(channel.Reader))
            yield return timetable;

        solverTask.GetAwaiter().GetResult();
    }

    private static void ValidateDiaryReferences(GenerateRequest request)
    {
        if (request.Diaries is null) return;

        var groupIds = new HashSet<string>(request.Groups.Select(g => g.Id));
        var teacherIds = new HashSet<string>(request.Teachers.Select(t => t.Id));

        foreach (var diary in request.Diaries)
        {
            if (!groupIds.Contains(diary.GroupId) && !teacherIds.Contains(diary.TeacherId))
                throw new GeneratorValidationException(GeneratorValidationErrorCode.DiaryReferencesNotFound, "Diary references not found: group and teacher not found.");
            if (!groupIds.Contains(diary.GroupId))
                throw new GeneratorValidationException(GeneratorValidationErrorCode.GroupNotFound, $"Group not found: {diary.GroupId}.");
            if (!teacherIds.Contains(diary.TeacherId))
                throw new GeneratorValidationException(GeneratorValidationErrorCode.TeacherNotFound, $"Teacher not found: {diary.TeacherId}.");
        }
    }

    private static GeneratedTimetable CreateEmptyTimetable(GenerateRequest request)
    {
        return new GeneratedTimetable(
            new TimetableGrid(request.DateStart, request.DateEnd, request.TimeSlots, Array.Empty<TimetableGridSchedule>()),
            0
        );
    }

    private static void SolveAndWriteToChannel(
        ChannelWriter<GeneratedTimetable> writer,
        GenerationContext generationContext,
        GenerateRequest request)
    {
        try
        {
            long? previousScore = null;
            var producedAny = false;

            do
            {
                var solver = new CpSolver { StringParameters = "enumerate_all_solutions:true" };

                var solutionPrinter = new GeneratorSolutionCallback(
                    generationContext,
                    timetable =>
                    {
                        producedAny = true;
                        writer.TryWrite(timetable);
                    }
                );

                if (previousScore != null)
                    OptimizeResult(generationContext, previousScore - 1);

                var sat = solver.Solve(generationContext.CpModel, solutionPrinter);

                if (sat is CpSolverStatus.Feasible or CpSolverStatus.Optimal)
                    previousScore = (long)solver.ObjectiveValue;
                else
                    previousScore = 0;
            } while (previousScore > 0);

            if (!producedAny)
                writer.TryWrite(CreateEmptyTimetable(request));
        }
        finally
        {
            writer.Complete();
        }
    }

    private static IEnumerable<GeneratedTimetable> ReadChannel(ChannelReader<GeneratedTimetable> reader)
    {
        while (reader.WaitToReadAsync().AsTask().GetAwaiter().GetResult())
        {
            while (reader.TryRead(out var item))
                yield return item;
        }
    }

    /// <summary>
    ///     Optimizes the result to be as satisfactory as possible according to
    ///     group scheduling preferences and teacher preferences.
    ///     When multiple valid solutions exist, this objective function guides
    ///     the solver towards the best one.
    /// </summary>
    private static void OptimizeResult(
        GenerationContext context,
        long? scoreLimit = null
    )
    {
        var qualityScore = LinearExpr.NewBuilder();

        AddBasicScheduleScore(qualityScore, context);

        var previousTimetableGrid = context.GenerateRequest.PreviousTimetableGrid;
        if (previousTimetableGrid is not null)
            AddPreviousTimetableBonus(qualityScore, context, previousTimetableGrid);

        if (scoreLimit != null)
            context.CpModel.Add(qualityScore <= context.CpModel.NewConstant((long)scoreLimit));

        context.CpModel.Maximize(qualityScore);
        context.Score = qualityScore;
    }

    private static void AddBasicScheduleScore(LinearExprBuilder qualityScore, GenerationContext context)
    {
        foreach (var proposal in context.AllProposals)
            qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, 1);
    }

    private static void AddPreviousTimetableBonus(
        LinearExprBuilder qualityScore,
        GenerationContext context,
        TimetableGrid previousTimetableGrid)
    {
        var request = context.GenerateRequest;

        foreach (var previousSchedule in previousTimetableGrid.Schedules)
        {
            var matchingProposals = context.AllProposals
                .Where(p => p.GroupId == previousSchedule.GroupId
                         && p.DiaryId == previousSchedule.DiaryId
                         && p.TeacherId == previousSchedule.TeacherId)
                .ToArray();

            AddScoreTerms(qualityScore, matchingProposals,
                p => p.Date.DayOfWeek == previousSchedule.Date.DayOfWeek,
                request.BoostSameDayOfWeekOnly);

            AddScoreTerms(qualityScore, matchingProposals,
                p => p.TimeSlot == previousSchedule.TimeSlot,
                request.BoostSameTimeSlotOnly);

            AddScoreTerms(qualityScore, matchingProposals,
                p => p.Date.DayOfWeek == previousSchedule.Date.DayOfWeek
                  && p.TimeSlot == previousSchedule.TimeSlot,
                request.BoostSameDayOfWeekAndTimeSlot);

            AddDayDistanceBonus(qualityScore, matchingProposals, previousSchedule, request);
            AddTimeSlotDistanceBonus(qualityScore, matchingProposals, previousSchedule, request);
        }
    }

    private static void AddScoreTerms(
        LinearExprBuilder qualityScore,
        GenerationContextScheduleProposal[] proposals,
        Func<GenerationContextScheduleProposal, bool> filter,
        long boost)
    {
        foreach (var proposal in proposals.Where(filter))
            qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, boost);
    }

    private static void AddDayDistanceBonus(
        LinearExprBuilder qualityScore,
        GenerationContextScheduleProposal[] matchingProposals,
        TimetableGridSchedule previousSchedule,
        GenerateRequest request)
    {
        var grouped = matchingProposals.GroupBy(p => p.Date.DayOfWeek);

        foreach (var group in grouped)
        {
            var distance = Math.Abs((int)group.Key - (int)previousSchedule.Date.DayOfWeek);
            var score = (7 - distance) * request.BoostLesserDistanceFromDayOfWeek;

            foreach (var proposal in group)
                qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, score);
        }
    }

    private static void AddTimeSlotDistanceBonus(
        LinearExprBuilder qualityScore,
        GenerationContextScheduleProposal[] matchingProposals,
        TimetableGridSchedule previousSchedule,
        GenerateRequest request)
    {
        var grouped = matchingProposals.GroupBy(p => p.TimeSlot);

        foreach (var group in grouped)
        {
            var distanceMinutes = previousSchedule.TimeSlot.Distance(group.Key).TotalMinutes;
            var score = (long)((-distanceMinutes) * request.BoostLesserDistanceFromTimeSlot);

            foreach (var proposal in group)
                qualityScore.AddTerm((IntVar)proposal.ModelBoolVar, score);
        }
    }
}