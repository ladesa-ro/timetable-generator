using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Application.Services;
using Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable;
using Ladesa.TimetableGenerator.Domain.Models.Availability.Abstractions;
using LinearExpr = Google.OrTools.Sat.LinearExpr;

namespace Ladesa.TimetableGenerator.Infrastructure.Solver.Generator;

public class GenerationContext
{
    public GenerationContext(
        GenerateTimetableCommand generateTimetableCommand,
        IAvailabilityEvaluator availabilityEvaluator,
        ICombinationGenerator combinationGenerator)
    {
        GenerateTimetableCommand = generateTimetableCommand;
        InitializeProposals(availabilityEvaluator, combinationGenerator);
    }

    public GenerateTimetableCommand GenerateTimetableCommand { get; }
    public CpModel CpModel { get; } = new();
    public List<GenerationContextScheduleProposal> AllProposals { get; } = [];

    public LinearExpr? Score { set; get; }

    private void InitializeProposals(
        IAvailabilityEvaluator availabilityEvaluator,
        ICombinationGenerator combinationGenerator)
    {
        AllProposals.Clear();

        foreach (var scheduleCombination in combinationGenerator.GetAllCombinationsWithAvailability(GenerateTimetableCommand, availabilityEvaluator))
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
