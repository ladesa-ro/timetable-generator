namespace Ladesa.TimetableGenerator.Domain.Models;

public interface IGenerationContext
{
    GenerateRequest GenerateRequest { get; }
    IReadOnlyList<IScheduleProposal> AllProposals { get; }
}
