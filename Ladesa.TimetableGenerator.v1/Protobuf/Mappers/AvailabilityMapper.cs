namespace Ladesa.TimetableGenerator.v1.Protobuf.Mappers;

public static class AvailabilityMapper
{
    public static Core.Domain.ValueObjects.IAvailabilityRule ToCoreDomainValueObject(AvailabilityRule dto)
    {
        switch (dto.RuleCase)
        {
            case AvailabilityRule.RuleOneofCase.Compount:
            {
                return new Core.Domain.ValueObjects.AvailabilityRuleCompound(
                    dto.Compount.Rules.Select(ToCoreDomainValueObject).ToArray()
                );
            }

            case AvailabilityRule.RuleOneofCase.UnavailableWeekDay:
            {
                return new Core.Domain.ValueObjects.AvailabilityRuleUnavailableWeekDay(
                    WeekDayMapper.ToCoreDomainValueObject(dto.UnavailableWeekDay.WeekDay),
                    TimeSlotMapper.ToCoreDomainValueObject(dto.UnavailableWeekDay.Slot)
                );
            }

            case AvailabilityRule.RuleOneofCase.UnavailableWeekDays:
            {
                return new Core.Domain.ValueObjects.AvailabilityRuleUnavailableWeekDays(
                    dto.UnavailableWeekDays.WeekDays.Select(WeekDayMapper.ToCoreDomainValueObject).ToArray(),
                    TimeSlotMapper.ToCoreDomainValueObject(dto.UnavailableWeekDays.Slot)
                );
            }

            case AvailabilityRule.RuleOneofCase.UnavailableTimeSlot:
            {
                return new Core.Domain.ValueObjects.AvailabilityRuleUnavailableTimeSlot(
                    TimeSlotMapper.ToCoreDomainValueObject(dto.UnavailableTimeSlot.Slot)
                );
            }


            case AvailabilityRule.RuleOneofCase.UnavailableSpecificDate:
            {
                return new Core.Domain.ValueObjects.AvailabilityRuleUnavailableSpecificDate(
                    DateOnly.Parse(dto.UnavailableSpecificDate.Date),
                    TimeSlotMapper.ToCoreDomainValueObject(dto.UnavailableSpecificDate.Slot)
                );
            }

            case AvailabilityRule.RuleOneofCase.UnavailableDateRange:
            {
                return new Core.Domain.ValueObjects.AvailabilityRuleUnavailableDateRange(
                    DateOnly.Parse(dto.UnavailableDateRange.DateStart),
                    DateOnly.Parse(dto.UnavailableDateRange.DateEnd),
                    TimeSlotMapper.ToCoreDomainValueObject(dto.UnavailableDateRange.Slot)
                );
            }

            case AvailabilityRule.RuleOneofCase.UnavailableMonthDay:
            {
                return new Core.Domain.ValueObjects.AvailabilityRuleUnavailableMonthDay(
                    dto.UnavailableMonthDay.MonthDay,
                    TimeSlotMapper.ToCoreDomainValueObject(dto.UnavailableMonthDay.Slot)
                );
            }

            case AvailabilityRule.RuleOneofCase.UnavailableYearMonths:
            {
                return new Core.Domain.ValueObjects.AvailabilityRuleUnavailableYearMonths(
                    dto.UnavailableYearMonths.Months.ToArray(),
                    TimeSlotMapper.ToCoreDomainValueObject(dto.UnavailableYearMonths.Slot)
                );
            }

            default:
            {
                return new Core.Domain.ValueObjects.AvailabilityRuleCompound([]);
            }
        }
    }

    public static AvailabilityRule ToProtobuf(Core.Domain.ValueObjects.IAvailabilityRule domain)
    {
        switch (domain)
        {
            case Core.Domain.ValueObjects.AvailabilityRuleCompound rule:
            {
                var compound = new AvailabilityRuleCompound
                {
                    Type = AvailabilityRuleType.Compound
                };

                compound.Rules.AddRange(rule.Rules.Select(ToProtobuf).ToArray());

                return new AvailabilityRule
                {
                    Type = compound.Type,
                    Compount = compound
                };
            }

            case Core.Domain.ValueObjects.AvailabilityRuleUnavailableWeekDay rule:
            {
                var weekDay = new AvailabilityRuleUnavailableWeekDay
                {
                    Type = AvailabilityRuleType.UnavailableWeekDay,
                    WeekDay = WeekDayMapper.ToProtobuf(rule.WeekDay),
                    Slot = TimeSlotMapper.ToProtobuf(rule.TimeSlot)
                };

                return new AvailabilityRule
                {
                    Type = weekDay.Type,
                    UnavailableWeekDay = weekDay
                };
            }


            case Core.Domain.ValueObjects.AvailabilityRuleUnavailableWeekDays rule:
            {
                var weekDays = new AvailabilityRuleUnavailableWeekDays
                {
                    Type = AvailabilityRuleType.UnavailableWeekDays,
                    Slot = TimeSlotMapper.ToProtobuf(rule.TimeSlot)
                };

                weekDays.WeekDays.AddRange(rule.WeekDays.Select(WeekDayMapper.ToProtobuf).ToArray());

                return new AvailabilityRule
                {
                    Type = weekDays.Type,
                    UnavailableWeekDays = weekDays
                };
            }

            case Core.Domain.ValueObjects.AvailabilityRuleUnavailableTimeSlot rule:
            {
                var timeSlot = new AvailabilityRuleUnavailableTimeSlot
                {
                    Type = AvailabilityRuleType.UnavailableTimeSlot,
                    Slot = TimeSlotMapper.ToProtobuf(rule.TimeSlot)
                };

                return new AvailabilityRule
                {
                    Type = timeSlot.Type,
                    UnavailableTimeSlot = timeSlot
                };
            }

            case Core.Domain.ValueObjects.AvailabilityRuleUnavailableSpecificDate rule:
            {
                var specificDate = new AvailabilityRuleUnavailableSpecificDate
                {
                    Type = AvailabilityRuleType.UnavailableSpecificDate,
                    Date = rule.Date.ToString(),
                    Slot = TimeSlotMapper.ToProtobuf(rule.TimeSlot)
                };

                return new AvailabilityRule
                {
                    Type = specificDate.Type,
                    UnavailableSpecificDate = specificDate
                };
            }

            case Core.Domain.ValueObjects.AvailabilityRuleUnavailableDateRange rule:
            {
                var dateRange = new AvailabilityRuleUnavailableDateRange
                {
                    Type = AvailabilityRuleType.UnavailableSpecificDate,
                    DateStart = rule.Start.ToString(),
                    DateEnd = rule.End.ToString(),
                    Slot = TimeSlotMapper.ToProtobuf(rule.TimeSlot)
                };

                return new AvailabilityRule
                {
                    Type = dateRange.Type,
                    UnavailableDateRange = dateRange
                };
            }


            case Core.Domain.ValueObjects.AvailabilityRuleUnavailableMonthDay rule:
            {
                var monthDay = new AvailabilityRuleUnavailableMonthDay
                {
                    Type = AvailabilityRuleType.UnavailableMonthDay,
                    MonthDay = rule.MonthDay,
                    Slot = TimeSlotMapper.ToProtobuf(rule.TimeSlot)
                };

                return new AvailabilityRule
                {
                    Type = monthDay.Type,
                    UnavailableMonthDay = monthDay
                };
            }


            case Core.Domain.ValueObjects.AvailabilityRuleUnavailableYearMonths rule:
            {
                var yearMonths = new AvailabilityRuleUnavailableYearMonths
                {
                    Type = AvailabilityRuleType.UnavaiableYearMonths,
                    Slot = TimeSlotMapper.ToProtobuf(rule.TimeSlot)
                };

                yearMonths.Months.AddRange(rule.Months);

                return new AvailabilityRule
                {
                    Type = yearMonths.Type,
                    UnavailableYearMonths = yearMonths
                };
            }

            default:
            {
                var compound = new AvailabilityRuleCompound
                {
                    Type = AvailabilityRuleType.Compound
                };

                compound.Rules.AddRange([]);

                return new AvailabilityRule
                {
                    Type = compound.Type,
                    Compount = compound
                };
            }
        }
    }
}