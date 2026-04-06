using Ladesa.TimetableGenerator.Domain.Abstractions.Entities;
using Ladesa.TimetableGenerator.Domain.Models.Diary;

namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Extensions;

public static class GenerateRequestExtensionDiaries
{
    public static Diary? DiaryFindById(this GenerateTimetableCommand timetableCommand, string diaryId)
        => timetableCommand.Diaries.FindById(diaryId);

    public static Diary DiaryFindByIdStrict(this GenerateTimetableCommand timetableCommand, string diaryId)
        => timetableCommand.Diaries.FindByIdStrict(diaryId);

    public static IEnumerable<Diary> DiaryFindByGroupId(this GenerateTimetableCommand timetableCommand, string groupId)
        => timetableCommand.Diaries.Where(diary => diary.GroupId == groupId);
}
