using System.Threading.Channels;
using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Application.Abstractions;
using Ladesa.TimetableGenerator.Application.Todo;
using Ladesa.TimetableGenerator.Application.Todo.Generator;
using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Domain.Commands;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;
using Ladesa.TimetableGenerator.Domain.Generator.GenerateRequest;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using Ladesa.TimetableGenerator.Domain.Models.Constraints;
using Ladesa.TimetableGenerator.Domain.Models.TimetableGrid;
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
    ///     Generates timetable solutions for the given timetableCommand, iteratively improving
    ///     quality. Yields results as they are found by the solver.
    /// </summary>
    public IEnumerable<GenerateTimetableCommandResponse> GenerateTimetables(
        GenerateTimetableCommand timetableCommand,
        IAvailabilityEvaluator availabilityEvaluator)
    {
        ValidateDiaryReferences(timetableCommand);

        var generationContext = CreateContextWithRestrictionsApplied(timetableCommand, availabilityEvaluator);

        if (generationContext.AllProposals.Count == 0)
        {
            yield return CreateEmptyTimetable(timetableCommand);
            yield break;
        }

        var channel = Channel.CreateUnbounded<GenerateTimetableCommandResponse>();
        var solverTask = Task.Run(() => SolveAndWriteToChannel(channel.Writer, generationContext, timetableCommand));

        foreach (var timetable in ReadChannel(channel.Reader))
            yield return timetable;

        solverTask.GetAwaiter().GetResult();
    }

    /// <summary>
    ///     Generates all possible schedule combinations, filtering by availability.
    /// </summary>
    public IEnumerable<GenerationScheduleCombination> GetAllCombinationsWithAvailability(
        GenerateTimetableCommand generateTimetableCommand,
        IAvailabilityEvaluator availabilityEvaluator)
        => _combinationGenerator.GetAllCombinationsWithAvailability(
            generateTimetableCommand, availabilityEvaluator);

    private static IConstraint[] BuildConstraints(GenerateTimetableCommand timetableCommand)
    {
        var enabledKinds = timetableCommand.EnabledConstraints ?? AllConstraintKinds;
        return enabledKinds
            .Where(ConstraintFactories.ContainsKey)
            .Select(kind => ConstraintFactories[kind]())
            .ToArray();
    }

    private GenerationContext CreateContextWithRestrictionsApplied(
        GenerateTimetableCommand timetableCommand,
        IAvailabilityEvaluator availabilityEvaluator)
    {
        var generationContext = new GenerationContext(timetableCommand, availabilityEvaluator, _combinationGenerator);

        foreach (var constraint in BuildConstraints(timetableCommand))
            constraint.Apply(generationContext);

        _optimizer.OptimizeResult(generationContext);

        return generationContext;
    }

    private static void ValidateDiaryReferences(GenerateTimetableCommand timetableCommand)
    {
        if (timetableCommand.Diaries is null) return;

        var groupIds = new HashSet<string>(timetableCommand.Groups.Select(g => g.Id));
        var teacherIds = new HashSet<string>(timetableCommand.Teachers.Select(t => t.Id));

        foreach (var diary in timetableCommand.Diaries)
        {
            if (!groupIds.Contains(diary.GroupId) && !teacherIds.Contains(diary.TeacherId))
                throw new GeneratorValidationException(GeneratorValidationErrorCode.DiaryReferencesNotFound, "Diary references not found: group and teacher not found.");
            if (!groupIds.Contains(diary.GroupId))
                throw new GeneratorValidationException(GeneratorValidationErrorCode.GroupNotFound, $"Group not found: {diary.GroupId}.");
            if (!teacherIds.Contains(diary.TeacherId))
                throw new GeneratorValidationException(GeneratorValidationErrorCode.TeacherNotFound, $"Teacher not found: {diary.TeacherId}.");
        }
    }

    private static GenerateTimetableCommandResponse CreateEmptyTimetable(GenerateTimetableCommand timetableCommand)
    {
        return new GenerateTimetableCommandResponse(
            new TimetableGrid(timetableCommand.DateStart, timetableCommand.DateEnd, timetableCommand.TimeSlots, Array.Empty<TimetableGridSchedule>()),
            0
        );
    }

    private void SolveAndWriteToChannel(
        ChannelWriter<GenerateTimetableCommandResponse> writer,
        GenerationContext generationContext,
        GenerateTimetableCommand timetableCommand)
    {
        try
        {
            var producedAny = RunSolverIterations(writer, generationContext);

            if (!producedAny)
                writer.TryWrite(CreateEmptyTimetable(timetableCommand));
        }
        finally
        {
            writer.Complete();
        }
    }

    private bool RunSolverIterations(
        ChannelWriter<GenerateTimetableCommandResponse> writer,
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
        ChannelWriter<GenerateTimetableCommandResponse> writer,
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

    private static IEnumerable<GenerateTimetableCommandResponse> ReadChannel(ChannelReader<GenerateTimetableCommandResponse> reader)
    {
        while (reader.WaitToReadAsync().AsTask().GetAwaiter().GetResult())
        {
            while (reader.TryRead(out var item))
                yield return item;
        }
    }
}
