using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

/// <summary>Holder for reflection information generated from entities/teacher-dto.proto</summary>
public static partial class TeacherDtoReflection {

    #region Descriptor
    /// <summary>File descriptor for entities/teacher-dto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TeacherDtoReflection() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
                "ChplbnRpdGllcy90ZWFjaGVyLWR0by5wcm90bxIcTGFkZXNhLlRpbWV0YWJs",
                "ZUdlbmVyYXRvci52MRokdmFsdWUtb2JqZWN0cy9hdmFpbGFiaWxpdHktZHRv",
                "LnByb3RvIl0KClRlYWNoZXJEdG8SCgoCaWQYASABKAkSQwoMYXZhaWxhYmls",
                "aXR5GAIgASgLMi0uTGFkZXNhLlRpbWV0YWJsZUdlbmVyYXRvci52MS5BdmFp",
                "bGFiaWxpdHlEdG9iBnByb3RvMw=="));
        descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
            new pbr::FileDescriptor[] { global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityDtoReflection.Descriptor, },
            new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TeacherDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TeacherDto.Parser, new[]{ "Id", "Availability" }, null, null, null, null)
            }));
    }
    #endregion

}