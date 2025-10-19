namespace Ladesa.TimetableGenerator.v1.Core.Domain.ValueObjects;

public enum AvailabilityType
{
    Compound,
    UnavailableWeekDay,
    UnavailableWeekDays,
    UnavailableTimeSlot,
    UnavailableSpecificDate,
    UnavailableDateRange,
    UnavailableMonthDay,
    UnavailableYearMonths
}