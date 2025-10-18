namespace Ladesa.TimetableGenerator.Core.Domain.ValueObjects;

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