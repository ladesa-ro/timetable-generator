namespace Ladesa.TimetableGenerator.v1.Core.Domain;

public static class GenerateRequestExtensionGroups
{
    public static Group? GroupFindById(this GenerateRequest request, string turmaId)
    {
        var turma = request.Groups.ToList().Find(turma => turma.Id == turmaId);
        return turma;
    }

    public static Group GroupFindByIdStrict(
        this GenerateRequest request,
        string turmaId
    )
    {
        var group = GroupFindById(request, turmaId);

        return group ?? throw new Exception($"Group not found: {turmaId}.");
    }
}