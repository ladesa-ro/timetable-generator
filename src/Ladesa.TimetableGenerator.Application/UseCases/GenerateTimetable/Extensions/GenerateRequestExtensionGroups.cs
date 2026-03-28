using Ladesa.TimetableGenerator.Domain.Abstractions.Entities;
using Ladesa.TimetableGenerator.Domain.Models.Group;

namespace Ladesa.TimetableGenerator.Application.UseCases.GenerateTimetable.Extensions;

public static class GenerateRequestExtensionGroups
{
    public static Group? GroupFindById(this GenerateTimetableCommand timetableCommand, string groupId)
        => timetableCommand.Groups.FindById(groupId);

    public static Group GroupFindByIdStrict(this GenerateTimetableCommand timetableCommand, string groupId)
        => timetableCommand.Groups.FindByIdStrict(groupId);
}
