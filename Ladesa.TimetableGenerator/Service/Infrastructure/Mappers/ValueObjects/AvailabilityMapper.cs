using System.Xml;
using Ladesa.TimetableGenerator.Core.Timetable.Domain.ValueObjects;
using Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Mappers.ValueObjects;

public static class AvailabilityMapper
{
    public static AvailabilityRule ToDomain(AvailabilityDto dto)
    {
        switch (dto.RegraCase)
        {
            case AvailabilityDto.RegraOneofCase.Compount:
            {
                return new AvailabilityRuleCompound(
                    dto.Compount.Rules.Select(ToDomain).ToArray()
                );
            }

            case AvailabilityDto.RegraOneofCase.UnavailableWeekDay:
            {
                return new AvailabilityRuleUnavailableWeekDay(
                    WeekDayMapper.ToDomain(dto.UnavailableWeekDay.WeekDay),
                    TimeSlotMapper.ToDomain(dto.UnavailableWeekDay.Slot)
                );
            }

            case AvailabilityDto.RegraOneofCase.UnavailableWeekDays:
            {
                return new AvailabilityRuleUnavailableWeekDays(
                    dto.UnavailableWeekDays.WeekDays.Select(WeekDayMapper.ToDomain).ToArray(),
                    TimeSlotMapper.ToDomain(dto.UnavailableWeekDays.Slot)
                );
            }

            case AvailabilityDto.RegraOneofCase.UnavailableTimeSlot:
            {
                return new AvailabilityRuleUnavailableTimeSlot(
                    TimeSlotMapper.ToDomain(dto.UnavailableTimeSlot.Slot)
                );
            }


            case AvailabilityDto.RegraOneofCase.UnavailableSpecificDate:
            {
                return new AvailabilityRuleUnavailableSpecificDate(
                    DateOnly.Parse(dto.UnavailableSpecificDate.Date),
                    TimeSlotMapper.ToDomain(dto.UnavailableSpecificDate.Slot)
                );
            }

            case AvailabilityDto.RegraOneofCase.UnavailableDateRange:
            {
                return new AvailabilityRuleUnavailableDateRange(
                    DateOnly.Parse(dto.UnavailableDateRange.DateStart),
                    DateOnly.Parse(dto.UnavailableDateRange.DateEnd),
                    TimeSlotMapper.ToDomain(dto.UnavailableDateRange.Slot)
                );
            }

            case AvailabilityDto.RegraOneofCase.UnavailableMonthDay:
            {
                return new AvailabilityRuleUnavailableMonthDay(
                    dto.UnavailableMonthDay.MonthDay,
                    TimeSlotMapper.ToDomain(dto.UnavailableMonthDay.Slot)
                );
            }

            case AvailabilityDto.RegraOneofCase.UnavailableYearMonths:
            {
                return new AvailabilityRuleUnavailableYearMonths(
                    dto.UnavailableYearMonths.Months.ToArray(),
                    TimeSlotMapper.ToDomain(dto.UnavailableYearMonths.Slot)
                );
            }

            default:
            {
                return new AvailabilityRuleCompound([]);
            }
        }
    }

    public static AvailabilityDto ToDto(AvailabilityRule domain)
    {
        switch (domain)
        {
            case AvailabilityRuleCompound rule:
            {
                var compound = new AvailabilityCompoundDto
                {
                    Type = AvailabilityTypeDto.Compound
                };

                compound.Rules.AddRange(rule.Rules.Select(ToDto).ToArray());

                return new AvailabilityDto
                {
                    Type = compound.Type,
                    Compount = compound
                };
            }

            case AvailabilityRuleUnavailableWeekDay rule:
            {
                var weekDay = new AvailabilityUnavailableWeekDayDto
                {
                    Type = AvailabilityTypeDto.UnavailableWeekDay,
                    WeekDay = WeekDayMapper.ToDto(rule.WeekDay),
                    Slot = TimeSlotMapper.ToDto(rule.TimeSlot)
                };

                return new AvailabilityDto
                {
                    Type = weekDay.Type,
                    UnavailableWeekDay = weekDay
                };
            }


            case AvailabilityRuleUnavailableWeekDays rule:
            {
                var weekDays = new AvailabilityUnavailableWeekDaysDto
                {
                    Type = AvailabilityTypeDto.UnavailableWeekDays,
                    Slot = TimeSlotMapper.ToDto(rule.TimeSlot)
                };

                weekDays.WeekDays.AddRange(rule.WeekDays.Select(WeekDayMapper.ToDto).ToArray());

                return new AvailabilityDto
                {
                    Type = weekDays.Type,
                    UnavailableWeekDays = weekDays
                };
            }

            case AvailabilityRuleUnavailableTimeSlot rule:
            {
                var timeSlot = new AvailabilityUnavailableTimeSlotDto
                {
                    Type = AvailabilityTypeDto.UnavailableTimeSlot,
                    Slot = TimeSlotMapper.ToDto(rule.TimeSlot)
                };

                return new AvailabilityDto
                {
                    Type = timeSlot.Type,
                    UnavailableTimeSlot = timeSlot
                };
            }

            case AvailabilityRuleUnavailableSpecificDate rule:
            {
                var specificDate = new AvailabilityUnavailableSpecificDateDto
                {
                    Type = AvailabilityTypeDto.UnavailableSpecificDate,
                    Date = rule.Date.ToString(),
                    Slot = TimeSlotMapper.ToDto(rule.TimeSlot)
                };

                return new AvailabilityDto
                {
                    Type = specificDate.Type,
                    UnavailableSpecificDate = specificDate
                };
            }

            case AvailabilityRuleUnavailableDateRange rule:
            {
                var dateRange = new AvailabilityUnavailableDateRangeDto
                {
                    Type = AvailabilityTypeDto.UnavailableSpecificDate,
                    DateStart = rule.Start.ToString(),
                    DateEnd = rule.End.ToString(),
                    Slot = TimeSlotMapper.ToDto(rule.TimeSlot)
                };

                return new AvailabilityDto
                {
                    Type = dateRange.Type,
                    UnavailableDateRange = dateRange
                };
            }


            case AvailabilityRuleUnavailableMonthDay rule:
            {
                var monthDay = new AvailabilityUnavailableMonthDayDto
                {
                    Type = AvailabilityTypeDto.UnavailableMonthDay,
                    MonthDay = rule.MonthDay,
                    Slot = TimeSlotMapper.ToDto(rule.TimeSlot)
                };

                return new AvailabilityDto
                {
                    Type = monthDay.Type,
                    UnavailableMonthDay = monthDay
                };
            }


            case AvailabilityRuleUnavailableYearMonths rule:
            {
                var yearMonths = new AvailabilityUnavailableYearMonthsDto
                {
                    Type = AvailabilityTypeDto.UnavaiableYearMonths,
                    Slot = TimeSlotMapper.ToDto(rule.TimeSlot)
                };

                yearMonths.Months.AddRange(rule.Months);

                return new AvailabilityDto
                {
                    Type = yearMonths.Type,
                    UnavailableYearMonths = yearMonths
                };
            }

            default:
            {
                var compound = new AvailabilityCompoundDto
                {
                    Type = AvailabilityTypeDto.Compound
                };

                compound.Rules.AddRange([]);

                return new AvailabilityDto
                {
                    Type = compound.Type,
                    Compount = compound
                };
            }
        }
    }
}