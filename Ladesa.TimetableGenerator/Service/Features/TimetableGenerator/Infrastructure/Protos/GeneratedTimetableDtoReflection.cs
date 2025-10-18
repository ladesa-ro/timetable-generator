using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

/// <summary>Holder for reflection information generated from entities/generated-timetable-dto.proto</summary>
public static partial class GeneratedTimetableDtoReflection {

    #region Descriptor
    /// <summary>File descriptor for entities/generated-timetable-dto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GeneratedTimetableDtoReflection() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
                "CiZlbnRpdGllcy9nZW5lcmF0ZWQtdGltZXRhYmxlLWR0by5wcm90bxIcTGFk",
                "ZXNhLlRpbWV0YWJsZUdlbmVyYXRvci52MRohdmFsdWUtb2JqZWN0cy90aW1l",
                "LXNsb3QtZHRvLnByb3RvIqEBChtHZW5lcmF0ZWRUaW1ldGFibGVMZXNzb25E",
                "dG8SDAoEZGF0ZRgBIAEoCRIQCghkaWFyeV9pZBgCIAEoCRISCgp0ZWFjaGVy",
                "X2lkGAMgASgJEhAKCGdyb3VwX2lkGAQgASgJEjwKCXRpbWVfc2xvdBgFIAEo",
                "CzIpLkxhZGVzYS5UaW1ldGFibGVHZW5lcmF0b3IudjEuVGltZVNsb3REdG8i",
                "7QEKFUdlbmVyYXRlZFRpbWV0YWJsZUR0bxISCgpyZXF1ZXN0X2lkGAEgASgJ",
                "EhIKCmRhdGVfc3RhcnQYAiABKAkSEAoIZGF0ZV9lbmQYAyABKAkSPQoKdGlt",
                "ZV9zbG90cxgEIAMoCzIpLkxhZGVzYS5UaW1ldGFibGVHZW5lcmF0b3IudjEu",
                "VGltZVNsb3REdG8STAoJc2NoZWR1bGVzGAUgAygLMjkuTGFkZXNhLlRpbWV0",
                "YWJsZUdlbmVyYXRvci52MS5HZW5lcmF0ZWRUaW1ldGFibGVMZXNzb25EdG8S",
                "DQoFc2NvcmUYBiABKAViBnByb3RvMw=="));
        descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
            new pbr::FileDescriptor[] { global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TimeSlotDtoReflection.Descriptor, },
            new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratedTimetableLessonDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratedTimetableLessonDto.Parser, new[]{ "Date", "DiaryId", "TeacherId", "GroupId", "TimeSlot" }, null, null, null, null),
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratedTimetableDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratedTimetableDto.Parser, new[]{ "RequestId", "DateStart", "DateEnd", "TimeSlots", "Schedules", "Score" }, null, null, null, null)
            }));
    }
    #endregion

}