namespace Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

public record Professor(
    string Id, 
    IRegraDisponibilidade RegraDisponibilidade
);
