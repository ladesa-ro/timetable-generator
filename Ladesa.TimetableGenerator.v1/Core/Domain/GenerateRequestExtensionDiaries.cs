namespace Ladesa.TimetableGenerator.v1.Core.Domain;

public static class GenerateRequestExtensionDiaries
{
    private static Diary? DiaryFindById(this GenerateRequest request, string diarioId)
    {
        var diario = request.Diaries.ToList().Find(diario => diario.Id == diarioId);
        return diario;
    }

    public static Diary DiaryFindByIdStrict(
        this GenerateRequest request,
        string diarioId
    )
    {
        var diary = DiaryFindById(request, diarioId);

        return diary ?? throw new Exception($"Diary not found: {diarioId}.");
    }

    public static IEnumerable<Diary> DiaryFindByGroupId(this GenerateRequest request, string turmaId)
    {
        return request.Diaries.Where(diario => diario.GroupId == turmaId).ToList();
    }
}