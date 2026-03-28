using Ladesa.TimetableGenerator.Domain.Abstractions.Entities;
using Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Exceptions;
using Ladesa.TimetableGenerator.Domain.Models.Group;

namespace Ladesa.TimetableGenerator.Domain.Commands.GenerateTimetableCommand.Extensions;
public static class GenerateRequestExtensionGroups
{
    public static Group? GroupFindById(this GenerateTimetableCommand timetableCommand, string groupId)
        => timetableCommand.Groups.FindById(groupId);
    
    public static Group GroupFindByIdStrict(this GenerateTimetableCommand timetableCommand, string groupId)
        => timetableCommand.Groups.FindByIdStrict(groupId, GeneratorValidationErrorCode.GroupNotFound);
}
