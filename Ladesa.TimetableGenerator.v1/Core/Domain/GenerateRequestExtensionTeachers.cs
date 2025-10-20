namespace Ladesa.TimetableGenerator.v1.Core.Domain;

public static class GenerateRequestExtensionTeachers
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