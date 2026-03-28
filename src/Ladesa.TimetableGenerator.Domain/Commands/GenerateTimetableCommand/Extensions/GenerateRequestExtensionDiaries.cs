using Ladesa.TimetableGenerator.Domain.Abstractions.Entities;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;
using Ladesa.TimetableGenerator.Domain.Models.Diary;

namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Extensions;
public static class GenerateRequestExtensionDiaries
{
    public static Diary? DiaryFindById(this GenerateTimetableCommand timetableCommand, string diaryId)
        => timetableCommand.Diaries.FindById(diaryId);
    
    public static Diary DiaryFindByIdStrict(this GenerateTimetableCommand timetableCommand, string diaryId)
        => timetableCommand.Diaries.FindByIdStrict(diaryId, GeneratorValidationErrorCode.DiaryReferencesNotFound);
    
    public static IEnumerable<Diary> DiaryFindByGroupId(this GenerateTimetableCommand timetableCommand, string groupId)
        => timetableCommand.Diaries.Where(diary => diary.GroupId == groupId);
}
