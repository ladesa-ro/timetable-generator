using Ladesa.TimetableGenerator.Domain.Abstractions.Entities;
using Ladesa.TimetableGenerator.Domain.Models.Teacher;

namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Extensions;

public static class GenerateRequestExtensionTeachers
{
    public static Teacher? TeacherFindById(this GenerateTimetableCommand timetableCommand, string teacherId)
        => timetableCommand.Teachers.FindById(teacherId);

    public static Teacher TeacherFindByIdStrict(this GenerateTimetableCommand timetableCommand, string teacherId)
        => timetableCommand.Teachers.FindByIdStrict(teacherId);
}
