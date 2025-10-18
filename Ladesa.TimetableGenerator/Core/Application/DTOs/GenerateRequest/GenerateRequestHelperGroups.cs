using Ladesa.TimetableGenerator.Core.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Application.DTOs.GenerateRequest;

public static class GenerateRequestHelperGroups
{
    public static Group? GroupFindById(this GenerateRequest payload, string turmaId)
    {
        var turma = payload.Groups.ToList().Find(turma => turma.Id == turmaId);
        return turma;
    }

    public static Group GroupFindByIdStrict(
        this GenerateRequest payload,
        string turmaId
    )
    {
        var group = payload.GroupFindById(turmaId);

        if (group == null)
        {
            throw new Exception($"Group not found: {turmaId}.");
        }

        return group;
    }
}