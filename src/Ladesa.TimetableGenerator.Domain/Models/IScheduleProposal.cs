namespace Ladesa.TimetableGenerator.Domain.Models;

public interface IScheduleProposal
{
    string GroupId { get; }
    string DiaryId { get; }
    string TeacherId { get; }
    DateOnly Date { get; }
    TimeSlot TimeSlot { get; }
}
