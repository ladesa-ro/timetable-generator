using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

public enum AvailabilityTypeDto {
    [pbr::OriginalName("Compound")] Compound = 0,
    [pbr::OriginalName("UnavailableWeekDay")] UnavailableWeekDay = 1,
    [pbr::OriginalName("UnavailableWeekDays")] UnavailableWeekDays = 2,
    [pbr::OriginalName("UnavailableTimeSlot")] UnavailableTimeSlot = 3,
    [pbr::OriginalName("UnavailableSpecificDate")] UnavailableSpecificDate = 4,
    [pbr::OriginalName("UnavailableDateRange")] UnavailableDateRange = 5,
    [pbr::OriginalName("UnavailableMonthDay")] UnavailableMonthDay = 6,
    [pbr::OriginalName("UnavaiableYearMonths")] UnavaiableYearMonths = 7,
}