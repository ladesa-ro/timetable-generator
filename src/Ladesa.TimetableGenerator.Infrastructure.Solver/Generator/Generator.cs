using System.Threading.Channels;
using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Infrastructure.Solver.Constraints;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

/// <summary>
///     Main entry point for timetable generation. Orchestrates validation,
///     constraint application, optimization, and solution streaming.
/// </summary>
public class Generator : IGenerator
{
    private static readonly Dictionary<ConstraintKind, Func<IConstraint>> ConstraintFactories = new()
    {
        [ConstraintKind.GroupOneScheduleAtSameTime] = () => new ConstraintGroupOneScheduleAtSameTime(),
        [ConstraintKind.TeacherOneScheduleAtSameTime] = () => new ConstraintTeacherOneScheduleAtSameTime(),
        [ConstraintKind.DiaryLimitSchedulesInOneWeek] = () => new ConstraintDiaryLimitSchedulesInOneWeek(),
        [ConstraintKind.DiaryLimitRemaining] = () => new ConstraintDiaryLimitRemaining(),
        [ConstraintKind.TeacherLunch] = () => new ConstraintTeacherLunch(),
        [ConstraintKind.GroupLunch] = () => new ConstraintGroupLunch(),
        [ConstraintKind.TeacherNoOppositeTurns] = () => new ConstraintTeacherNoOppositeTurns(),
        [ConstraintKind.Teacher12Hours] = () => new ConstraintTeacher12Hours(),
        [ConstraintKind.GroupNoOverlappingTimeSlots] = () => new ConstraintGroupNoOverlappingTimeSlots(),
        [ConstraintKind.TeacherNoOverlappingTimeSlots] = () => new ConstraintTeacherNoOverlappingTimeSlots(),
    };

    private static readonly ConstraintKind[] AllConstraintKinds = Enum.GetValues<ConstraintKind>();

    private readonly ITimetableOptimizer _optimizer = new TimetableOptimizer();
    private readonly IScheduleCombinationGenerator _combinationGenerator;

    public Generator(IScheduleCombinationGenerator combinationGenerator)
    {
        _combinationGenerator = combinationGenerator;
    }

    /// <summary>
    ///     Generates timetable solutions for the given request, iteratively improving
    ///     quality. Yields results as they are found by the solver.
    /// </summary>
    public IEnumerable<GeneratedTimetable> GenerateTimetables(
        GenerateRequest request,
        IAvailabilityEvaluator availabilityEvaluator)
    {
        ValidateDiaryReferences(request);

        var generationContext = CreateContextWithRestrictionsApplied(request, availabilityEvaluator);

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
    /// </summary>
    public IEnumerable<GenerationScheduleCombination> GetAllCombinationsWithAvailability(
        GenerateRequest generateRequest,
        IAvailabilityEvaluator availabilityEvaluator)
        => _combinationGenerator.GetAllCombinationsWithAvailability(
            generateRequest, availabilityEvaluator);

    private static IConstraint[] BuildConstraints(GenerateRequest request)
    {
        var enabledKinds = request.EnabledConstraints ?? AllConstraintKinds;
        return enabledKinds
            .Where(ConstraintFactories.ContainsKey)
            .Select(kind => ConstraintFactories[kind]())
            .ToArray();
    }

    private GenerationContext CreateContextWithRestrictionsApplied(
        GenerateRequest request,
        IAvailabilityEvaluator availabilityEvaluator)
    {
        var generationContext = new GenerationContext(request, availabilityEvaluator, _combinationGenerator);

        foreach (var constraint in BuildConstraints(request))
            constraint.Apply(generationContext);

        _optimizer.OptimizeResult(generationContext);

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

    private void SolveAndWriteToChannel(
        ChannelWriter<GeneratedTimetable> writer,
        GenerationContext generationContext,
        GenerateRequest request)
    {
        try
        {
            var producedAny = RunSolverIterations(writer, generationContext);

            if (!producedAny)
                writer.TryWrite(CreateEmptyTimetable(request));
        }
        finally
        {
            writer.Complete();
        }
    }

    private bool RunSolverIterations(
        ChannelWriter<GeneratedTimetable> writer,
        GenerationContext generationContext)
    {
        long? previousScore = null;
        var producedAny = false;

        do
        {
            if (previousScore != null)
                _optimizer.OptimizeResult(generationContext, previousScore - 1);

            previousScore = SolveIteration(generationContext, writer, ref producedAny);
        } while (previousScore > 0);

        return producedAny;
    }

    private static long SolveIteration(
        GenerationContext generationContext,
        ChannelWriter<GeneratedTimetable> writer,
        ref bool producedAny)
    {
        var solver = new CpSolver { StringParameters = "enumerate_all_solutions:true" };
        var localProducedAny = producedAny;

        var solutionPrinter = new GeneratorSolutionCallback(
            generationContext,
            timetable =>
            {
                localProducedAny = true;
                writer.TryWrite(timetable);
            }
        );

        var sat = solver.Solve(generationContext.CpModel, solutionPrinter);
        producedAny = localProducedAny;

        return sat is CpSolverStatus.Feasible or CpSolverStatus.Optimal
            ? (long)solver.ObjectiveValue
            : 0;
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
