using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

/// <summary>Holder for reflection information generated from entities/group-dto.proto</summary>
public static partial class GroupDtoReflection {

    #region Descriptor
    /// <summary>File descriptor for entities/group-dto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GroupDtoReflection() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
                "ChhlbnRpdGllcy9ncm91cC1kdG8ucHJvdG8SHExhZGVzYS5UaW1ldGFibGVH",
                "ZW5lcmF0b3IudjEaJHZhbHVlLW9iamVjdHMvYXZhaWxhYmlsaXR5LWR0by5w",
                "cm90byJbCghHcm91cER0bxIKCgJpZBgBIAEoCRJDCgxhdmFpbGFiaWxpdHkY",
                "AiABKAsyLS5MYWRlc2EuVGltZXRhYmxlR2VuZXJhdG9yLnYxLkF2YWlsYWJp",
                "bGl0eUR0b2IGcHJvdG8z"));
        descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
            new pbr::FileDescriptor[] { global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityDtoReflection.Descriptor, },
            new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GroupDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GroupDto.Parser, new[]{ "Id", "Availability" }, null, null, null, null)
            }));
    }
    #endregion

}