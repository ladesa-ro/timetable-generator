using Google.Protobuf;
using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class AvailabilityRule : IMessage<AvailabilityRule>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<AvailabilityRule> _parser = new(() => new AvailabilityRule());
    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<AvailabilityRule> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => AvailabilityRuleReflection.Descriptor.MessageTypes[0];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRule()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRule(AvailabilityRule other) : this()
    {
        type_ = other.type_;
        switch (other.RuleCase)
        {
            case RuleOneofCase.Compount:
                Compount = other.Compount.Clone();
                break;
            case RuleOneofCase.UnavailableWeekDay:
                UnavailableWeekDay = other.UnavailableWeekDay.Clone();
                break;
            case RuleOneofCase.UnavailableWeekDays:
                UnavailableWeekDays = other.UnavailableWeekDays.Clone();
                break;
            case RuleOneofCase.UnavailableTimeSlot:
                UnavailableTimeSlot = other.UnavailableTimeSlot.Clone();
                break;
            case RuleOneofCase.UnavailableSpecificDate:
                UnavailableSpecificDate = other.UnavailableSpecificDate.Clone();
                break;
            case RuleOneofCase.UnavailableDateRange:
                UnavailableDateRange = other.UnavailableDateRange.Clone();
                break;
            case RuleOneofCase.UnavailableMonthDay:
                UnavailableMonthDay = other.UnavailableMonthDay.Clone();
                break;
            case RuleOneofCase.UnavailableYearMonths:
                UnavailableYearMonths = other.UnavailableYearMonths.Clone();
                break;
        }

        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRule Clone()
    {
        return new AvailabilityRule(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;

    private AvailabilityRuleType type_ = AvailabilityRuleType.Compound;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleType Type
    {
        get => type_;
        set => type_ = value;
    }

    /// <summary>Field number for the "compount" field.</summary>
    public const int CompountFieldNumber = 2;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleCompound Compount
    {
        get => ruleCase_ == RuleOneofCase.Compount ? (AvailabilityRuleCompound)rule_ : null;
        set
        {
            rule_ = value;
            ruleCase_ = value == null ? RuleOneofCase.None : RuleOneofCase.Compount;
        }
    }

    /// <summary>Field number for the "unavailable_week_day" field.</summary>
    public const int UnavailableWeekDayFieldNumber = 3;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableWeekDay UnavailableWeekDay
    {
        get => ruleCase_ == RuleOneofCase.UnavailableWeekDay ? (AvailabilityRuleUnavailableWeekDay)rule_ : null;
        set
        {
            rule_ = value;
            ruleCase_ = value == null ? RuleOneofCase.None : RuleOneofCase.UnavailableWeekDay;
        }
    }

    /// <summary>Field number for the "unavailable_week_days" field.</summary>
    public const int UnavailableWeekDaysFieldNumber = 4;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableWeekDays UnavailableWeekDays
    {
        get => ruleCase_ == RuleOneofCase.UnavailableWeekDays ? (AvailabilityRuleUnavailableWeekDays)rule_ : null;
        set
        {
            rule_ = value;
            ruleCase_ = value == null ? RuleOneofCase.None : RuleOneofCase.UnavailableWeekDays;
        }
    }

    /// <summary>Field number for the "unavailable_time_slot" field.</summary>
    public const int UnavailableTimeSlotFieldNumber = 5;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableTimeSlot UnavailableTimeSlot
    {
        get => ruleCase_ == RuleOneofCase.UnavailableTimeSlot ? (AvailabilityRuleUnavailableTimeSlot)rule_ : null;
        set
        {
            rule_ = value;
            ruleCase_ = value == null ? RuleOneofCase.None : RuleOneofCase.UnavailableTimeSlot;
        }
    }

    /// <summary>Field number for the "unavailable_specific_date" field.</summary>
    public const int UnavailableSpecificDateFieldNumber = 6;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableSpecificDate UnavailableSpecificDate
    {
        get => ruleCase_ == RuleOneofCase.UnavailableSpecificDate
            ? (AvailabilityRuleUnavailableSpecificDate)rule_
            : null;
        set
        {
            rule_ = value;
            ruleCase_ = value == null ? RuleOneofCase.None : RuleOneofCase.UnavailableSpecificDate;
        }
    }

    /// <summary>Field number for the "unavailable_date_range" field.</summary>
    public const int UnavailableDateRangeFieldNumber = 7;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableDateRange UnavailableDateRange
    {
        get => ruleCase_ == RuleOneofCase.UnavailableDateRange ? (AvailabilityRuleUnavailableDateRange)rule_ : null;
        set
        {
            rule_ = value;
            ruleCase_ = value == null ? RuleOneofCase.None : RuleOneofCase.UnavailableDateRange;
        }
    }

    /// <summary>Field number for the "unavailable_month_day" field.</summary>
    public const int UnavailableMonthDayFieldNumber = 8;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableMonthDay UnavailableMonthDay
    {
        get => ruleCase_ == RuleOneofCase.UnavailableMonthDay ? (AvailabilityRuleUnavailableMonthDay)rule_ : null;
        set
        {
            rule_ = value;
            ruleCase_ = value == null ? RuleOneofCase.None : RuleOneofCase.UnavailableMonthDay;
        }
    }

    /// <summary>Field number for the "unavailable_year_months" field.</summary>
    public const int UnavailableYearMonthsFieldNumber = 9;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableYearMonths UnavailableYearMonths
    {
        get => ruleCase_ == RuleOneofCase.UnavailableYearMonths ? (AvailabilityRuleUnavailableYearMonths)rule_ : null;
        set
        {
            rule_ = value;
            ruleCase_ = value == null ? RuleOneofCase.None : RuleOneofCase.UnavailableYearMonths;
        }
    }

    private object rule_;

    /// <summary>Enum of possible cases for the "rule" oneof.</summary>
    public enum RuleOneofCase
    {
        None = 0,
        Compount = 2,
        UnavailableWeekDay = 3,
        UnavailableWeekDays = 4,
        UnavailableTimeSlot = 5,
        UnavailableSpecificDate = 6,
        UnavailableDateRange = 7,
        UnavailableMonthDay = 8,
        UnavailableYearMonths = 9
    }

    private RuleOneofCase ruleCase_ = RuleOneofCase.None;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RuleOneofCase RuleCase => ruleCase_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearRule()
    {
        ruleCase_ = RuleOneofCase.None;
        rule_ = null;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as AvailabilityRule);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AvailabilityRule other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (Type != other.Type) return false;
        if (!Equals(Compount, other.Compount)) return false;
        if (!Equals(UnavailableWeekDay, other.UnavailableWeekDay)) return false;
        if (!Equals(UnavailableWeekDays, other.UnavailableWeekDays)) return false;
        if (!Equals(UnavailableTimeSlot, other.UnavailableTimeSlot)) return false;
        if (!Equals(UnavailableSpecificDate, other.UnavailableSpecificDate)) return false;
        if (!Equals(UnavailableDateRange, other.UnavailableDateRange)) return false;
        if (!Equals(UnavailableMonthDay, other.UnavailableMonthDay)) return false;
        if (!Equals(UnavailableYearMonths, other.UnavailableYearMonths)) return false;
        if (RuleCase != other.RuleCase) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (Type != AvailabilityRuleType.Compound) hash ^= Type.GetHashCode();
        if (ruleCase_ == RuleOneofCase.Compount) hash ^= Compount.GetHashCode();
        if (ruleCase_ == RuleOneofCase.UnavailableWeekDay) hash ^= UnavailableWeekDay.GetHashCode();
        if (ruleCase_ == RuleOneofCase.UnavailableWeekDays) hash ^= UnavailableWeekDays.GetHashCode();
        if (ruleCase_ == RuleOneofCase.UnavailableTimeSlot) hash ^= UnavailableTimeSlot.GetHashCode();
        if (ruleCase_ == RuleOneofCase.UnavailableSpecificDate) hash ^= UnavailableSpecificDate.GetHashCode();
        if (ruleCase_ == RuleOneofCase.UnavailableDateRange) hash ^= UnavailableDateRange.GetHashCode();
        if (ruleCase_ == RuleOneofCase.UnavailableMonthDay) hash ^= UnavailableMonthDay.GetHashCode();
        if (ruleCase_ == RuleOneofCase.UnavailableYearMonths) hash ^= UnavailableYearMonths.GetHashCode();
        hash ^= (int)ruleCase_;
        if (_unknownFields != null) hash ^= _unknownFields.GetHashCode();
        return hash;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString()
    {
        return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(CodedOutputStream output)
    {
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        output.WriteRawMessage(this);
#else
      if (Type != global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleType.Compound) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (ruleCase_ == RuleOneofCase.Compount) {
        output.WriteRawTag(18);
        output.WriteMessage(Compount);
      }
      if (ruleCase_ == RuleOneofCase.UnavailableWeekDay) {
        output.WriteRawTag(26);
        output.WriteMessage(UnavailableWeekDay);
      }
      if (ruleCase_ == RuleOneofCase.UnavailableWeekDays) {
        output.WriteRawTag(34);
        output.WriteMessage(UnavailableWeekDays);
      }
      if (ruleCase_ == RuleOneofCase.UnavailableTimeSlot) {
        output.WriteRawTag(42);
        output.WriteMessage(UnavailableTimeSlot);
      }
      if (ruleCase_ == RuleOneofCase.UnavailableSpecificDate) {
        output.WriteRawTag(50);
        output.WriteMessage(UnavailableSpecificDate);
      }
      if (ruleCase_ == RuleOneofCase.UnavailableDateRange) {
        output.WriteRawTag(58);
        output.WriteMessage(UnavailableDateRange);
      }
      if (ruleCase_ == RuleOneofCase.UnavailableMonthDay) {
        output.WriteRawTag(66);
        output.WriteMessage(UnavailableMonthDay);
      }
      if (ruleCase_ == RuleOneofCase.UnavailableYearMonths) {
        output.WriteRawTag(74);
        output.WriteMessage(UnavailableYearMonths);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
#endif
    }

#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void IBufferMessage.InternalWriteTo(ref WriteContext output)
    {
        if (Type != AvailabilityRuleType.Compound)
        {
            output.WriteRawTag(8);
            output.WriteEnum((int)Type);
        }

        if (ruleCase_ == RuleOneofCase.Compount)
        {
            output.WriteRawTag(18);
            output.WriteMessage(Compount);
        }

        if (ruleCase_ == RuleOneofCase.UnavailableWeekDay)
        {
            output.WriteRawTag(26);
            output.WriteMessage(UnavailableWeekDay);
        }

        if (ruleCase_ == RuleOneofCase.UnavailableWeekDays)
        {
            output.WriteRawTag(34);
            output.WriteMessage(UnavailableWeekDays);
        }

        if (ruleCase_ == RuleOneofCase.UnavailableTimeSlot)
        {
            output.WriteRawTag(42);
            output.WriteMessage(UnavailableTimeSlot);
        }

        if (ruleCase_ == RuleOneofCase.UnavailableSpecificDate)
        {
            output.WriteRawTag(50);
            output.WriteMessage(UnavailableSpecificDate);
        }

        if (ruleCase_ == RuleOneofCase.UnavailableDateRange)
        {
            output.WriteRawTag(58);
            output.WriteMessage(UnavailableDateRange);
        }

        if (ruleCase_ == RuleOneofCase.UnavailableMonthDay)
        {
            output.WriteRawTag(66);
            output.WriteMessage(UnavailableMonthDay);
        }

        if (ruleCase_ == RuleOneofCase.UnavailableYearMonths)
        {
            output.WriteRawTag(74);
            output.WriteMessage(UnavailableYearMonths);
        }

        if (_unknownFields != null) _unknownFields.WriteTo(ref output);
    }
#endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize()
    {
        var size = 0;
        if (Type != AvailabilityRuleType.Compound) size += 1 + pb::CodedOutputStream.ComputeEnumSize((int)Type);
        if (ruleCase_ == RuleOneofCase.Compount) size += 1 + pb::CodedOutputStream.ComputeMessageSize(Compount);
        if (ruleCase_ == RuleOneofCase.UnavailableWeekDay)
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(UnavailableWeekDay);
        if (ruleCase_ == RuleOneofCase.UnavailableWeekDays)
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(UnavailableWeekDays);
        if (ruleCase_ == RuleOneofCase.UnavailableTimeSlot)
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(UnavailableTimeSlot);
        if (ruleCase_ == RuleOneofCase.UnavailableSpecificDate)
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(UnavailableSpecificDate);
        if (ruleCase_ == RuleOneofCase.UnavailableDateRange)
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(UnavailableDateRange);
        if (ruleCase_ == RuleOneofCase.UnavailableMonthDay)
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(UnavailableMonthDay);
        if (ruleCase_ == RuleOneofCase.UnavailableYearMonths)
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(UnavailableYearMonths);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AvailabilityRule other)
    {
        if (other == null) return;
        if (other.Type != AvailabilityRuleType.Compound) Type = other.Type;
        switch (other.RuleCase)
        {
            case RuleOneofCase.Compount:
                if (Compount == null) Compount = new AvailabilityRuleCompound();
                Compount.MergeFrom(other.Compount);
                break;
            case RuleOneofCase.UnavailableWeekDay:
                if (UnavailableWeekDay == null) UnavailableWeekDay = new AvailabilityRuleUnavailableWeekDay();
                UnavailableWeekDay.MergeFrom(other.UnavailableWeekDay);
                break;
            case RuleOneofCase.UnavailableWeekDays:
                if (UnavailableWeekDays == null) UnavailableWeekDays = new AvailabilityRuleUnavailableWeekDays();
                UnavailableWeekDays.MergeFrom(other.UnavailableWeekDays);
                break;
            case RuleOneofCase.UnavailableTimeSlot:
                if (UnavailableTimeSlot == null) UnavailableTimeSlot = new AvailabilityRuleUnavailableTimeSlot();
                UnavailableTimeSlot.MergeFrom(other.UnavailableTimeSlot);
                break;
            case RuleOneofCase.UnavailableSpecificDate:
                if (UnavailableSpecificDate == null)
                    UnavailableSpecificDate = new AvailabilityRuleUnavailableSpecificDate();
                UnavailableSpecificDate.MergeFrom(other.UnavailableSpecificDate);
                break;
            case RuleOneofCase.UnavailableDateRange:
                if (UnavailableDateRange == null) UnavailableDateRange = new AvailabilityRuleUnavailableDateRange();
                UnavailableDateRange.MergeFrom(other.UnavailableDateRange);
                break;
            case RuleOneofCase.UnavailableMonthDay:
                if (UnavailableMonthDay == null) UnavailableMonthDay = new AvailabilityRuleUnavailableMonthDay();
                UnavailableMonthDay.MergeFrom(other.UnavailableMonthDay);
                break;
            case RuleOneofCase.UnavailableYearMonths:
                if (UnavailableYearMonths == null) UnavailableYearMonths = new AvailabilityRuleUnavailableYearMonths();
                UnavailableYearMonths.MergeFrom(other.UnavailableYearMonths);
                break;
        }

        _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CodedInputStream input)
    {
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        input.ReadRawMessage(this);
#else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Type = (global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleType) input.ReadEnum();
            break;
          }
          case 18: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleCompound subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleCompound();
            if (ruleCase_ == RuleOneofCase.Compount) {
              subBuilder.MergeFrom(Compount);
            }
            input.ReadMessage(subBuilder);
            Compount = subBuilder;
            break;
          }
          case 26: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableWeekDay subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableWeekDay();
            if (ruleCase_ == RuleOneofCase.UnavailableWeekDay) {
              subBuilder.MergeFrom(UnavailableWeekDay);
            }
            input.ReadMessage(subBuilder);
            UnavailableWeekDay = subBuilder;
            break;
          }
          case 34: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableWeekDays subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableWeekDays();
            if (ruleCase_ == RuleOneofCase.UnavailableWeekDays) {
              subBuilder.MergeFrom(UnavailableWeekDays);
            }
            input.ReadMessage(subBuilder);
            UnavailableWeekDays = subBuilder;
            break;
          }
          case 42: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableTimeSlot subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableTimeSlot();
            if (ruleCase_ == RuleOneofCase.UnavailableTimeSlot) {
              subBuilder.MergeFrom(UnavailableTimeSlot);
            }
            input.ReadMessage(subBuilder);
            UnavailableTimeSlot = subBuilder;
            break;
          }
          case 50: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableSpecificDate subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableSpecificDate();
            if (ruleCase_ == RuleOneofCase.UnavailableSpecificDate) {
              subBuilder.MergeFrom(UnavailableSpecificDate);
            }
            input.ReadMessage(subBuilder);
            UnavailableSpecificDate = subBuilder;
            break;
          }
          case 58: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableDateRange subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableDateRange();
            if (ruleCase_ == RuleOneofCase.UnavailableDateRange) {
              subBuilder.MergeFrom(UnavailableDateRange);
            }
            input.ReadMessage(subBuilder);
            UnavailableDateRange = subBuilder;
            break;
          }
          case 66: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableMonthDay subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableMonthDay();
            if (ruleCase_ == RuleOneofCase.UnavailableMonthDay) {
              subBuilder.MergeFrom(UnavailableMonthDay);
            }
            input.ReadMessage(subBuilder);
            UnavailableMonthDay = subBuilder;
            break;
          }
          case 74: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableYearMonths subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.AvailabilityRuleUnavailableYearMonths();
            if (ruleCase_ == RuleOneofCase.UnavailableYearMonths) {
              subBuilder.MergeFrom(UnavailableYearMonths);
            }
            input.ReadMessage(subBuilder);
            UnavailableYearMonths = subBuilder;
            break;
          }
        }
      }
#endif
    }

#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void IBufferMessage.InternalMergeFrom(ref ParseContext input)
    {
        uint tag;
        while ((tag = input.ReadTag()) != 0)
            switch (tag)
            {
                default:
                    _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                    break;
                case 8:
                {
                    Type = (AvailabilityRuleType)input.ReadEnum();
                    break;
                }
                case 18:
                {
                    var subBuilder = new AvailabilityRuleCompound();
                    if (ruleCase_ == RuleOneofCase.Compount) subBuilder.MergeFrom(Compount);
                    input.ReadMessage(subBuilder);
                    Compount = subBuilder;
                    break;
                }
                case 26:
                {
                    var subBuilder = new AvailabilityRuleUnavailableWeekDay();
                    if (ruleCase_ == RuleOneofCase.UnavailableWeekDay) subBuilder.MergeFrom(UnavailableWeekDay);
                    input.ReadMessage(subBuilder);
                    UnavailableWeekDay = subBuilder;
                    break;
                }
                case 34:
                {
                    var subBuilder = new AvailabilityRuleUnavailableWeekDays();
                    if (ruleCase_ == RuleOneofCase.UnavailableWeekDays) subBuilder.MergeFrom(UnavailableWeekDays);
                    input.ReadMessage(subBuilder);
                    UnavailableWeekDays = subBuilder;
                    break;
                }
                case 42:
                {
                    var subBuilder = new AvailabilityRuleUnavailableTimeSlot();
                    if (ruleCase_ == RuleOneofCase.UnavailableTimeSlot) subBuilder.MergeFrom(UnavailableTimeSlot);
                    input.ReadMessage(subBuilder);
                    UnavailableTimeSlot = subBuilder;
                    break;
                }
                case 50:
                {
                    var subBuilder = new AvailabilityRuleUnavailableSpecificDate();
                    if (ruleCase_ == RuleOneofCase.UnavailableSpecificDate)
                        subBuilder.MergeFrom(UnavailableSpecificDate);
                    input.ReadMessage(subBuilder);
                    UnavailableSpecificDate = subBuilder;
                    break;
                }
                case 58:
                {
                    var subBuilder = new AvailabilityRuleUnavailableDateRange();
                    if (ruleCase_ == RuleOneofCase.UnavailableDateRange) subBuilder.MergeFrom(UnavailableDateRange);
                    input.ReadMessage(subBuilder);
                    UnavailableDateRange = subBuilder;
                    break;
                }
                case 66:
                {
                    var subBuilder = new AvailabilityRuleUnavailableMonthDay();
                    if (ruleCase_ == RuleOneofCase.UnavailableMonthDay) subBuilder.MergeFrom(UnavailableMonthDay);
                    input.ReadMessage(subBuilder);
                    UnavailableMonthDay = subBuilder;
                    break;
                }
                case 74:
                {
                    var subBuilder = new AvailabilityRuleUnavailableYearMonths();
                    if (ruleCase_ == RuleOneofCase.UnavailableYearMonths) subBuilder.MergeFrom(UnavailableYearMonths);
                    input.ReadMessage(subBuilder);
                    UnavailableYearMonths = subBuilder;
                    break;
                }
            }
    }
#endif
}