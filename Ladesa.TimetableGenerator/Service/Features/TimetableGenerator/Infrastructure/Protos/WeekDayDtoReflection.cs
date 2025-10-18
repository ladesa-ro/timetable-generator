using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

/// <summary>Holder for reflection information generated from value-objects/week-day-dto.proto</summary>
public static partial class WeekDayDtoReflection {

    #region Descriptor
    /// <summary>File descriptor for value-objects/week-day-dto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static WeekDayDtoReflection() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
                "CiB2YWx1ZS1vYmplY3RzL3dlZWstZGF5LWR0by5wcm90bxIcTGFkZXNhLlRp",
                "bWV0YWJsZUdlbmVyYXRvci52MSpoCgpXZWVrRGF5RHRvEgoKBlN1bmRheRAA",
                "EgoKBk1vbmRheRABEgsKB1R1ZXNkYXkQAhINCglXZWRuZXNkYXkQAxIMCghU",
                "aHVyc2RheRAEEgoKBkZyaWRheRAFEgwKCFNhdHVyZGF5EAZiBnByb3RvMw=="));
        descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
            new pbr::FileDescriptor[] { },
            new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.WeekDayDto), }, null, null));
    }
    #endregion

}