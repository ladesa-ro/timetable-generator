using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

/// <summary>Holder for reflection information generated from value-objects/availability-dto.proto</summary>
public static partial class AvailabilityDtoReflection {

    #region Descriptor
    /// <summary>File descriptor for value-objects/availability-dto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
        get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AvailabilityDtoReflection() {
        byte[] descriptorData = global::System.Convert.FromBase64String(
            string.Concat(
                "CiR2YWx1ZS1vYmplY3RzL2F2YWlsYWJpbGl0eS1kdG8ucHJvdG8SHExhZGVz",
                "YS5UaW1ldGFibGVHZW5lcmF0b3IudjEaIHZhbHVlLW9iamVjdHMvd2Vlay1k",
                "YXktZHRvLnByb3RvGiF2YWx1ZS1vYmplY3RzL3RpbWUtc2xvdC1kdG8ucHJv",
                "dG8i5wYKD0F2YWlsYWJpbGl0eUR0bxI/CgR0eXBlGAEgASgOMjEuTGFkZXNh",
                "LlRpbWV0YWJsZUdlbmVyYXRvci52MS5BdmFpbGFiaWxpdHlUeXBlRHRvEkkK",
                "CGNvbXBvdW50GAIgASgLMjUuTGFkZXNhLlRpbWV0YWJsZUdlbmVyYXRvci52",
                "MS5BdmFpbGFiaWxpdHlDb21wb3VuZER0b0gAEl8KFHVuYXZhaWxhYmxlX3dl",
                "ZWtfZGF5GAMgASgLMj8uTGFkZXNhLlRpbWV0YWJsZUdlbmVyYXRvci52MS5B",
                "dmFpbGFiaWxpdHlVbmF2YWlsYWJsZVdlZWtEYXlEdG9IABJhChV1bmF2YWls",
                "YWJsZV93ZWVrX2RheXMYBCABKAsyQC5MYWRlc2EuVGltZXRhYmxlR2VuZXJh",
                "dG9yLnYxLkF2YWlsYWJpbGl0eVVuYXZhaWxhYmxlV2Vla0RheXNEdG9IABJh",
                "ChV1bmF2YWlsYWJsZV90aW1lX3Nsb3QYBSABKAsyQC5MYWRlc2EuVGltZXRh",
                "YmxlR2VuZXJhdG9yLnYxLkF2YWlsYWJpbGl0eVVuYXZhaWxhYmxlVGltZVNs",
                "b3REdG9IABJpChl1bmF2YWlsYWJsZV9zcGVjaWZpY19kYXRlGAYgASgLMkQu",
                "TGFkZXNhLlRpbWV0YWJsZUdlbmVyYXRvci52MS5BdmFpbGFiaWxpdHlVbmF2",
                "YWlsYWJsZVNwZWNpZmljRGF0ZUR0b0gAEmMKFnVuYXZhaWxhYmxlX2RhdGVf",
                "cmFuZ2UYByABKAsyQS5MYWRlc2EuVGltZXRhYmxlR2VuZXJhdG9yLnYxLkF2",
                "YWlsYWJpbGl0eVVuYXZhaWxhYmxlRGF0ZVJhbmdlRHRvSAASYQoVdW5hdmFp",
                "bGFibGVfbW9udGhfZGF5GAggASgLMkAuTGFkZXNhLlRpbWV0YWJsZUdlbmVy",
                "YXRvci52MS5BdmFpbGFiaWxpdHlVbmF2YWlsYWJsZU1vbnRoRGF5RHRvSAAS",
                "ZQoXdW5hdmFpbGFibGVfeWVhcl9tb250aHMYCSABKAsyQi5MYWRlc2EuVGlt",
                "ZXRhYmxlR2VuZXJhdG9yLnYxLkF2YWlsYWJpbGl0eVVuYXZhaWxhYmxlWWVh",
                "ck1vbnRoc0R0b0gAQgcKBXJlZ3JhIpgBChdBdmFpbGFiaWxpdHlDb21wb3Vu",
                "ZER0bxI/CgR0eXBlGAEgASgOMjEuTGFkZXNhLlRpbWV0YWJsZUdlbmVyYXRv",
                "ci52MS5BdmFpbGFiaWxpdHlUeXBlRHRvEjwKBXJ1bGVzGAIgAygLMi0uTGFk",
                "ZXNhLlRpbWV0YWJsZUdlbmVyYXRvci52MS5BdmFpbGFiaWxpdHlEdG8i2QEK",
                "IUF2YWlsYWJpbGl0eVVuYXZhaWxhYmxlV2Vla0RheUR0bxI/CgR0eXBlGAEg",
                "ASgOMjEuTGFkZXNhLlRpbWV0YWJsZUdlbmVyYXRvci52MS5BdmFpbGFiaWxp",
                "dHlUeXBlRHRvEjoKCHdlZWtfZGF5GAIgASgOMiguTGFkZXNhLlRpbWV0YWJs",
                "ZUdlbmVyYXRvci52MS5XZWVrRGF5RHRvEjcKBHNsb3QYAyABKAsyKS5MYWRl",
                "c2EuVGltZXRhYmxlR2VuZXJhdG9yLnYxLlRpbWVTbG90RHRvItsBCiJBdmFp",
                "bGFiaWxpdHlVbmF2YWlsYWJsZVdlZWtEYXlzRHRvEj8KBHR5cGUYASABKA4y",
                "MS5MYWRlc2EuVGltZXRhYmxlR2VuZXJhdG9yLnYxLkF2YWlsYWJpbGl0eVR5",
                "cGVEdG8SOwoJd2Vla19kYXlzGAIgAygOMiguTGFkZXNhLlRpbWV0YWJsZUdl",
                "bmVyYXRvci52MS5XZWVrRGF5RHRvEjcKBHNsb3QYAyABKAsyKS5MYWRlc2Eu",
                "VGltZXRhYmxlR2VuZXJhdG9yLnYxLlRpbWVTbG90RHRvIp4BCiJBdmFpbGFi",
                "aWxpdHlVbmF2YWlsYWJsZVRpbWVTbG90RHRvEj8KBHR5cGUYASABKA4yMS5M",
                "YWRlc2EuVGltZXRhYmxlR2VuZXJhdG9yLnYxLkF2YWlsYWJpbGl0eVR5cGVE",
                "dG8SNwoEc2xvdBgCIAEoCzIpLkxhZGVzYS5UaW1ldGFibGVHZW5lcmF0b3Iu",
                "djEuVGltZVNsb3REdG8isAEKJkF2YWlsYWJpbGl0eVVuYXZhaWxhYmxlU3Bl",
                "Y2lmaWNEYXRlRHRvEj8KBHR5cGUYASABKA4yMS5MYWRlc2EuVGltZXRhYmxl",
                "R2VuZXJhdG9yLnYxLkF2YWlsYWJpbGl0eVR5cGVEdG8SDAoEZGF0ZRgCIAEo",
                "CRI3CgRzbG90GAMgASgLMikuTGFkZXNhLlRpbWV0YWJsZUdlbmVyYXRvci52",
                "MS5UaW1lU2xvdER0byLFAQojQXZhaWxhYmlsaXR5VW5hdmFpbGFibGVEYXRl",
                "UmFuZ2VEdG8SPwoEdHlwZRgBIAEoDjIxLkxhZGVzYS5UaW1ldGFibGVHZW5l",
                "cmF0b3IudjEuQXZhaWxhYmlsaXR5VHlwZUR0bxISCgpkYXRlX3N0YXJ0GAIg",
                "ASgJEhAKCGRhdGVfZW5kGAMgASgJEjcKBHNsb3QYBCABKAsyKS5MYWRlc2Eu",
                "VGltZXRhYmxlR2VuZXJhdG9yLnYxLlRpbWVTbG90RHRvIrEBCiJBdmFpbGFi",
                "aWxpdHlVbmF2YWlsYWJsZU1vbnRoRGF5RHRvEj8KBHR5cGUYASABKA4yMS5M",
                "YWRlc2EuVGltZXRhYmxlR2VuZXJhdG9yLnYxLkF2YWlsYWJpbGl0eVR5cGVE",
                "dG8SEQoJbW9udGhfZGF5GAIgASgFEjcKBHNsb3QYAyABKAsyKS5MYWRlc2Eu",
                "VGltZXRhYmxlR2VuZXJhdG9yLnYxLlRpbWVTbG90RHRvIrABCiRBdmFpbGFi",
                "aWxpdHlVbmF2YWlsYWJsZVllYXJNb250aHNEdG8SPwoEdHlwZRgBIAEoDjIx",
                "LkxhZGVzYS5UaW1ldGFibGVHZW5lcmF0b3IudjEuQXZhaWxhYmlsaXR5VHlw",
                "ZUR0bxIOCgZtb250aHMYAiADKAUSNwoEc2xvdBgDIAEoCzIpLkxhZGVzYS5U",
                "aW1ldGFibGVHZW5lcmF0b3IudjEuVGltZVNsb3REdG8q1wEKE0F2YWlsYWJp",
                "bGl0eVR5cGVEdG8SDAoIQ29tcG91bmQQABIWChJVbmF2YWlsYWJsZVdlZWtE",
                "YXkQARIXChNVbmF2YWlsYWJsZVdlZWtEYXlzEAISFwoTVW5hdmFpbGFibGVU",
                "aW1lU2xvdBADEhsKF1VuYXZhaWxhYmxlU3BlY2lmaWNEYXRlEAQSGAoUVW5h",
                "dmFpbGFibGVEYXRlUmFuZ2UQBRIXChNVbmF2YWlsYWJsZU1vbnRoRGF5EAYS",
                "GAoUVW5hdmFpYWJsZVllYXJNb250aHMQB2IGcHJvdG8z"));
        descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
            new pbr::FileDescriptor[] { global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.WeekDayDtoReflection.Descriptor, global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TimeSlotDtoReflection.Descriptor, },
            new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto), }, null, new pbr::GeneratedClrTypeInfo[] {
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityDto.Parser, new[]{ "Type", "Compount", "UnavailableWeekDay", "UnavailableWeekDays", "UnavailableTimeSlot", "UnavailableSpecificDate", "UnavailableDateRange", "UnavailableMonthDay", "UnavailableYearMonths" }, new[]{ "Regra" }, null, null, null),
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityCompoundDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityCompoundDto.Parser, new[]{ "Type", "Rules" }, null, null, null, null),
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableWeekDayDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableWeekDayDto.Parser, new[]{ "Type", "WeekDay", "Slot" }, null, null, null, null),
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableWeekDaysDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableWeekDaysDto.Parser, new[]{ "Type", "WeekDays", "Slot" }, null, null, null, null),
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableTimeSlotDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableTimeSlotDto.Parser, new[]{ "Type", "Slot" }, null, null, null, null),
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableSpecificDateDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableSpecificDateDto.Parser, new[]{ "Type", "Date", "Slot" }, null, null, null, null),
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableDateRangeDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableDateRangeDto.Parser, new[]{ "Type", "DateStart", "DateEnd", "Slot" }, null, null, null, null),
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableMonthDayDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableMonthDayDto.Parser, new[]{ "Type", "MonthDay", "Slot" }, null, null, null, null),
                new pbr::GeneratedClrTypeInfo(typeof(global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableYearMonthsDto), global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityUnavailableYearMonthsDto.Parser, new[]{ "Type", "Months", "Slot" }, null, null, null, null)
            }));
    }
    #endregion

}