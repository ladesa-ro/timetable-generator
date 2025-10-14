using Ladesa.TimetableGenerator.Core.Features.Payload.Resources;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperProfessores
{
    public static Professor? FindById(IGeradorPayload payload, string professorId)
    {
        var professor = payload.Professores.ToList().Find(professor => professor.Id == professorId);
        return professor;
    }

    public static Professor FindByIdStrict(IGeradorPayload payload, string professorId)
    {
        var professor = FindById(payload, professorId);

        if (professor == null)
        {
            throw new Exception($"Professor não encontrado: {professorId}.");
        }

        return professor;
    }
}