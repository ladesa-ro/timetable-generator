using Ladesa.TimetableGenerator.Core.Timetable.Domain.Entities;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.Messages;

namespace Ladesa.TimetableGenerator.Core.Features.Payload.Helpers;

public class HelperProfessores
{
    public static Teacher? FindById(GeneratorPayload payload, string professorId)
    {
        var professor = payload.Teachers.ToList().Find(professor => professor.Id == professorId);
        return professor;
    }

    public static Teacher FindByIdStrict(GeneratorPayload payload, string professorId)
    {
        var professor = FindById(payload, professorId);

        if (professor == null)
            throw new Exception($"Professor não encontrado: {professorId}.");

        return professor;
    }
}