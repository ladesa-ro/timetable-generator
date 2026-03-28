using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Application.Todo;
using Ladesa.TimetableGenerator.Domain.Abstractions;
using Ladesa.TimetableGenerator.Domain.Commands;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand;
using Ladesa.TimetableGenerator.Domain.Generator.GenerateRequest;
using Ladesa.TimetableGenerator.Domain.Models;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using LinearExpr = Google.OrTools.Sat.LinearExpr;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

internal class GenerationContext
{
    public GenerationContext(
        GenerateTimetableCommand generateTimetableCommand,
        IAvailabilityEvaluator availabilityEvaluator,
        IScheduleCombinationGenerator scheduleCombinationGenerator)
    {
        GenerateTimetableCommand = generateTimetableCommand;
        InitializeProposals(availabilityEvaluator, scheduleCombinationGenerator);
    }

    public GenerateTimetableCommand GenerateTimetableCommand { get; }
    public CpModel CpModel { get; } = new();
    public List<GenerationContextScheduleProposal> AllProposals { get; } = [];

    public LinearExpr? Score { set; get; }

    private void InitializeProposals(
        IAvailabilityEvaluator availabilityEvaluator,
        IScheduleCombinationGenerator scheduleCombinationGenerator)
    {
        AllProposals.Clear();

        foreach (var scheduleCombination in scheduleCombinationGenerator.GetAllCombinationsWithAvailability(GenerateTimetableCommand, availabilityEvaluator))
        {
            var scheduleProposal = new GenerationContextScheduleProposal(
                this,
                scheduleCombination.GroupId,
                scheduleCombination.DiaryId,
                scheduleCombination.TeacherId,
                scheduleCombination.Date,
                scheduleCombination.TimeSlot
            );

            AllProposals.Add(scheduleProposal);
        }
    }
}
