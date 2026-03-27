using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Domain.Models;
using LinearExpr = Google.OrTools.Sat.LinearExpr;

namespace Ladesa.TimetableGenerator.Domain.Generator;

public class GenerationContext
{
    public GenerationContext(GenerateRequest generateRequest)
    {
        GenerateRequest = generateRequest;
        InitializeProposals();
    }

    public GenerateRequest GenerateRequest { get; }
    public CpModel CpModel { get; } = new();
    public List<GenerationContextScheduleProposal> AllProposals { get; } = [];

    public LinearExpr? Score { set; get; }

    private void InitializeProposals()
    {
        AllProposals.Clear();

        foreach (var scheduleCombination in Generator.GetAllCombinationsWithAvailability(GenerateRequest))
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