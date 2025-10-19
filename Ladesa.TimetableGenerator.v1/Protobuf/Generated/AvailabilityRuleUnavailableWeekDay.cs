using Google.Protobuf;
using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class AvailabilityRuleUnavailableWeekDay : IMessage<AvailabilityRuleUnavailableWeekDay>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<AvailabilityRuleUnavailableWeekDay> _parser = new(() =>
        new AvailabilityRuleUnavailableWeekDay());

    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<AvailabilityRuleUnavailableWeekDay> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => AvailabilityRuleReflection.Descriptor.MessageTypes[2];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableWeekDay()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableWeekDay(AvailabilityRuleUnavailableWeekDay other) : this()
    {
        type_ = other.type_;
        weekDay_ = other.weekDay_;
        slot_ = other.slot_ != null ? other.slot_.Clone() : null;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableWeekDay Clone()
    {
        return new AvailabilityRuleUnavailableWeekDay(this);
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

    /// <summary>Field number for the "week_day" field.</summary>
    public const int WeekDayFieldNumber = 2;

    private WeekDay weekDay_ = WeekDay.Sunday;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WeekDay WeekDay
    {
        get => weekDay_;
        set => weekDay_ = value;
    }

    /// <summary>Field number for the "slot" field.</summary>
    public const int SlotFieldNumber = 3;

    private TimeSlot slot_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimeSlot Slot
    {
        get => slot_;
        set => slot_ = value;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as AvailabilityRuleUnavailableWeekDay);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AvailabilityRuleUnavailableWeekDay other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (Type != other.Type) return false;
        if (WeekDay != other.WeekDay) return false;
        if (!Equals(Slot, other.Slot)) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (Type != AvailabilityRuleType.Compound) hash ^= Type.GetHashCode();
        if (WeekDay != WeekDay.Sunday) hash ^= WeekDay.GetHashCode();
        if (slot_ != null) hash ^= Slot.GetHashCode();
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
      if (WeekDay != global::Ladesa.TimetableGenerator.v1.Protobuf.WeekDay.Sunday) {
        output.WriteRawTag(16);
        output.WriteEnum((int) WeekDay);
      }
      if (slot_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Slot);
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

        if (WeekDay != WeekDay.Sunday)
        {
            output.WriteRawTag(16);
            output.WriteEnum((int)WeekDay);
        }

        if (slot_ != null)
        {
            output.WriteRawTag(26);
            output.WriteMessage(Slot);
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
        if (WeekDay != WeekDay.Sunday) size += 1 + pb::CodedOutputStream.ComputeEnumSize((int)WeekDay);
        if (slot_ != null) size += 1 + pb::CodedOutputStream.ComputeMessageSize(Slot);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AvailabilityRuleUnavailableWeekDay other)
    {
        if (other == null) return;
        if (other.Type != AvailabilityRuleType.Compound) Type = other.Type;
        if (other.WeekDay != WeekDay.Sunday) WeekDay = other.WeekDay;
        if (other.slot_ != null)
        {
            if (slot_ == null) Slot = new TimeSlot();
            Slot.MergeFrom(other.Slot);
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
          case 16: {
            WeekDay = (global::Ladesa.TimetableGenerator.v1.Protobuf.WeekDay) input.ReadEnum();
            break;
          }
          case 26: {
            if (slot_ == null) {
              Slot = new global::Ladesa.TimetableGenerator.v1.Protobuf.TimeSlot();
            }
            input.ReadMessage(Slot);
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
                case 16:
                {
                    WeekDay = (WeekDay)input.ReadEnum();
                    break;
                }
                case 26:
                {
                    if (slot_ == null) Slot = new TimeSlot();
                    input.ReadMessage(Slot);
                    break;
                }
            }
    }
#endif
}