using System.Diagnostics;
using Ladesa.TimetableGenerator.Core.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Application.DTOs.GenerateRequest;

public static class GenerateRequestHelperDiaries
{
    public static Diary? DiaryFindById(this GenerateRequest payload, string diarioId)
    {
        var diario = payload.Diaries.ToList().Find(diario => diario.Id == diarioId);
        return diario;
    }

    public static Diary DiaryFindByIdStrict(
        this GenerateRequest payload,
        string diarioId
    )
    {
        var diary = payload.DiaryFindById(diarioId);

        if (diary == null)
            throw new Exception($"Diary not found: {diarioId}.");

        return diary;
    }

    public static IEnumerable<Diary> DiaryFindByGroupId(this GenerateRequest payload, string turmaId)
    {
        return payload.Diaries.Where(diario => diario.GroupId == turmaId).ToList();
    }
}
