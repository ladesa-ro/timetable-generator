using Google.Protobuf;
using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class AvailabilityRuleUnavailableDateRange : IMessage<AvailabilityRuleUnavailableDateRange>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<AvailabilityRuleUnavailableDateRange> _parser = new(() =>
        new AvailabilityRuleUnavailableDateRange());

    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<AvailabilityRuleUnavailableDateRange> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => AvailabilityRuleReflection.Descriptor.MessageTypes[6];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableDateRange()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableDateRange(AvailabilityRuleUnavailableDateRange other) : this()
    {
        type_ = other.type_;
        dateStart_ = other.dateStart_;
        dateEnd_ = other.dateEnd_;
        slot_ = other.slot_ != null ? other.slot_.Clone() : null;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableDateRange Clone()
    {
        return new AvailabilityRuleUnavailableDateRange(this);
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

    /// <summary>Field number for the "date_start" field.</summary>
    public const int DateStartFieldNumber = 2;

    private string dateStart_ = "";

    /// <summary>
    /// date
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DateStart
    {
        get => dateStart_;
        set => dateStart_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "date_end" field.</summary>
    public const int DateEndFieldNumber = 3;

    private string dateEnd_ = "";

    /// <summary>
    /// date
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DateEnd
    {
        get => dateEnd_;
        set => dateEnd_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "slot" field.</summary>
    public const int SlotFieldNumber = 4;

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
        return Equals(other as AvailabilityRuleUnavailableDateRange);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AvailabilityRuleUnavailableDateRange other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (Type != other.Type) return false;
        if (DateStart != other.DateStart) return false;
        if (DateEnd != other.DateEnd) return false;
        if (!Equals(Slot, other.Slot)) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (Type != AvailabilityRuleType.Compound) hash ^= Type.GetHashCode();
        if (DateStart.Length != 0) hash ^= DateStart.GetHashCode();
        if (DateEnd.Length != 0) hash ^= DateEnd.GetHashCode();
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
      if (DateStart.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(DateStart);
      }
      if (DateEnd.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(DateEnd);
      }
      if (slot_ != null) {
        output.WriteRawTag(34);
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

        if (DateStart.Length != 0)
        {
            output.WriteRawTag(18);
            output.WriteString(DateStart);
        }

        if (DateEnd.Length != 0)
        {
            output.WriteRawTag(26);
            output.WriteString(DateEnd);
        }

        if (slot_ != null)
        {
            output.WriteRawTag(34);
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
        if (DateStart.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(DateStart);
        if (DateEnd.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(DateEnd);
        if (slot_ != null) size += 1 + pb::CodedOutputStream.ComputeMessageSize(Slot);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AvailabilityRuleUnavailableDateRange other)
    {
        if (other == null) return;
        if (other.Type != AvailabilityRuleType.Compound) Type = other.Type;
        if (other.DateStart.Length != 0) DateStart = other.DateStart;
        if (other.DateEnd.Length != 0) DateEnd = other.DateEnd;
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
          case 18: {
            DateStart = input.ReadString();
            break;
          }
          case 26: {
            DateEnd = input.ReadString();
            break;
          }
          case 34: {
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
                case 18:
                {
                    DateStart = input.ReadString();
                    break;
                }
                case 26:
                {
                    DateEnd = input.ReadString();
                    break;
                }
                case 34:
                {
                    if (slot_ == null) Slot = new TimeSlot();
                    input.ReadMessage(Slot);
                    break;
                }
            }
    }
#endif
}