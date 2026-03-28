using Ladesa.TimetableGenerator.Domain.Abstractions.Entities;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;
using Ladesa.TimetableGenerator.Domain.Models.Teacher;

namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Extensions;
public static class GenerateRequestExtensionTeachers
{
    public static Teacher? TeacherFindById(this GenerateTimetableCommand timetableCommand, string teacherId)
        => timetableCommand.Teachers.FindById(teacherId);
    
    public static Teacher TeacherFindByIdStrict(this GenerateTimetableCommand timetableCommand, string teacherId)
        => timetableCommand.Teachers.FindByIdStrict(teacherId, GeneratorValidationErrorCode.TeacherNotFound);
}
