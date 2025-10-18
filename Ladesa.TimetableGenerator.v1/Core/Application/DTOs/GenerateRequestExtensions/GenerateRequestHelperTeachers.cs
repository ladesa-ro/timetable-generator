using Ladesa.TimetableGenerator.v1.Core.Domain.Entities;

namespace Ladesa.TimetableGenerator.v1.Core.Application.DTOs.GenerateRequestExtensions;

public static class GenerateRequestHelperTeachers
{
    public static Teacher? TeacherFindById(this GenerateRequest request, string professorId)
    {
        var professor = request.Teachers.ToList().Find(professor => professor.Id == professorId);
        return professor;
    }

    public static Teacher TeacherFindByIdStrict(this GenerateRequest request, string professorId)
    {
        var teacher = TeacherFindById(request, professorId);

        return teacher ?? throw new Exception($"Teacher not found: {professorId}.");
    }
}