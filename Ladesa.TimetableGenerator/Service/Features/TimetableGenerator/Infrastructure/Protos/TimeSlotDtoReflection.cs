using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

/// <summary>Holder for reflection information generated from value-objects/time-slot-dto.proto</summary>
public static partial class TimeSlotDtoReflection {

    #region Descriptor
    /// <summary>File descriptor for value-objects/time-slot-dto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static TimeSlotDtoReflection() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
                "CiF2YWx1ZS1vYmplY3RzL3RpbWUtc2xvdC1kdG8ucHJvdG8SHExhZGVzYS5U",
                "aW1ldGFibGVHZW5lcmF0b3IudjEiKQoLVGltZVNsb3REdG8SDQoFc3RhcnQY",
                "ASABKAkSCwoDZW5kGAIgASgJYgZwcm90bzM="));
        descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
            new pbr::FileDescriptor[] { },
            new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TimeSlotDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TimeSlotDto.Parser, new[]{ "Start", "End" }, null, null, null, null)
            }));
    }
    #endregion

}