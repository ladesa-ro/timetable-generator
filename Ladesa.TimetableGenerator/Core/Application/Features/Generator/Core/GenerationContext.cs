using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.Core.Application.DTOs.GenerateRequest;
using LinearExpr = Google.OrTools.Sat.LinearExpr;

namespace Ladesa.TimetableGenerator.Core.Application.Features.Generator.Core;

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

    public LinearExpr? Score { get; set; }

    public void InitializeProposals()
    {
        AllProposals.Clear();

        foreach (var scheduleCombination in Generator.GetAllCombinationsWithAvailability(GenerateRequest))
        {
            var propostaDeAula = new GenerationContextScheduleProposal(
                this,
                scheduleCombination.GroupId,
                scheduleCombination.DiaryId,
                scheduleCombination.TeacherId,
                scheduleCombination.Date,
                scheduleCombination.TimeSlot
            );

            AllProposals.Add(propostaDeAula);
        }

        Console.WriteLine($"--> Quantidade máxima de possíveis combinações de aula: {AllProposals.Count} (desconsiderando restrições ou disponibilidades)");
    }
}