using System.Threading.Channels;
using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Domain.Constraints;
using Ladesa.TimetableGenerator.Domain.Models;

namespace Ladesa.TimetableGenerator.Domain.Generator;

/// <summary>
///     Main entry point for timetable generation. Orchestrates validation,
///     constraint application, optimization, and solution streaming.
/// </summary>
public static class Generator
{
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

    /// <summary>
    ///     Generates all possible schedule combinations, filtering by availability.
    ///     Delegates to <see cref="ScheduleCombinationGenerator"/>.
    /// </summary>
    public static IEnumerable<GenerationScheduleCombination> GetAllCombinationsWithAvailability(
        GenerateRequest generateRequest)
        => ScheduleCombinationGenerator.GetAllCombinationsWithAvailability(generateRequest);

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

        TimetableOptimizer.OptimizeResult(generationContext);

        return generationContext;
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
                    TimetableOptimizer.OptimizeResult(generationContext, previousScore - 1);

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
}
