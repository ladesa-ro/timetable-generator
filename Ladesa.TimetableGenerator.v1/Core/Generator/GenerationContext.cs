using Google.OrTools.Sat;
using Ladesa.TimetableGenerator.v1.Core.Domain;
using LinearExpr = Google.OrTools.Sat.LinearExpr;

namespace Ladesa.TimetableGenerator.v1.Core.Generator;

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

        Console.WriteLine(
            $"--> Quantidade máxima de possíveis combinações de aula: {AllProposals.Count} (desconsiderando restrições ou disponibilidades)");
    }
}