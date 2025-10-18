using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

/// <summary>Holder for reflection information generated from entities/subject-dto.proto</summary>
public static partial class SubjectDtoReflection {

    #region Descriptor
    /// <summary>File descriptor for entities/subject-dto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SubjectDtoReflection() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
                "ChplbnRpdGllcy9zdWJqZWN0LWR0by5wcm90bxIcTGFkZXNhLlRpbWV0YWJs",
                "ZUdlbmVyYXRvci52MSImCgpTdWJqZWN0RHRvEgoKAmlkGAEgASgJEgwKBG5h",
                "bWUYAiABKAliBnByb3RvMw=="));
        descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
            new pbr::FileDescriptor[] { },
            new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.SubjectDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.SubjectDto.Parser, new[]{ "Id", "Name" }, null, null, null, null)
            }));
    }
    #endregion

}