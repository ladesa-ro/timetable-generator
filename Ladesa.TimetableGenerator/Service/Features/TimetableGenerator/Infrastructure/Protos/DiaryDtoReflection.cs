using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

/// <summary>Holder for reflection information generated from entities/diary-dto.proto</summary>
public static partial class DiaryDtoReflection {

    #region Descriptor
    /// <summary>File descriptor for entities/diary-dto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static DiaryDtoReflection() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
                "ChhlbnRpdGllcy9kaWFyeS1kdG8ucHJvdG8SHExhZGVzYS5UaW1ldGFibGVH",
                "ZW5lcmF0b3IudjEidwoIRGlhcnlEdG8SCgoCaWQYASABKAkSEgoKc3ViamVj",
                "dF9pZBgCIAEoCRISCgp0ZWFjaGVyX2lkGAMgASgJEhAKCGdyb3VwX2lkGAQg",
                "ASgJEhIKCndlZWtfbGltaXQYBSABKAUSEQoJcmVtYWluaW5nGAYgASgFYgZw",
                "cm90bzM="));
        descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
            new pbr::FileDescriptor[] { },
            new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.DiaryDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.DiaryDto.Parser, new[]{ "Id", "SubjectId", "TeacherId", "GroupId", "WeekLimit", "Remaining" }, null, null, null, null)
            }));
    }
    #endregion

}