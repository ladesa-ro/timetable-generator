using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public enum WeekDay
{
    [pbr::OriginalName("Sunday")] Sunday = 0,
    [pbr::OriginalName("Monday")] Monday = 1,
    [pbr::OriginalName("Tuesday")] Tuesday = 2,
    [pbr::OriginalName("Wednesday")] Wednesday = 3,
    [pbr::OriginalName("Thursday")] Thursday = 4,
    [pbr::OriginalName("Friday")] Friday = 5,
    [pbr::OriginalName("Saturday")] Saturday = 6
}