using Ladesa.TimetableGenerator.Core.Domain.Entities;

namespace Ladesa.TimetableGenerator.Core.Application.DTOs.GenerateRequest;

public static class GenerateRequestHelperTeachers
{
    public static Teacher? TeacherFindById(this GenerateRequest payload, string professorId)
    {
        var professor = payload.Teachers.ToList().Find(professor => professor.Id == professorId);
        return professor;
    }

    public static Teacher TeacherFindByIdStrict(this GenerateRequest payload, string professorId)
    {
        var teacher = payload.TeacherFindById(professorId);

        if (teacher == null)
            throw new Exception($"Teacher not found: {professorId}.");

        return teacher;
    }
}