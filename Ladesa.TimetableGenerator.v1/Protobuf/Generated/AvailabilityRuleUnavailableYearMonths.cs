using Google.Protobuf;
using pb = Google.Protobuf;
using pbc = Google.Protobuf.Collections;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class AvailabilityRuleUnavailableYearMonths : IMessage<AvailabilityRuleUnavailableYearMonths>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<AvailabilityRuleUnavailableYearMonths> _parser = new(() =>
        new AvailabilityRuleUnavailableYearMonths());

    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<AvailabilityRuleUnavailableYearMonths> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => AvailabilityRuleReflection.Descriptor.MessageTypes[8];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableYearMonths()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableYearMonths(AvailabilityRuleUnavailableYearMonths other) : this()
    {
        type_ = other.type_;
        months_ = other.months_.Clone();
        slot_ = other.slot_ != null ? other.slot_.Clone() : null;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleUnavailableYearMonths Clone()
    {
        return new AvailabilityRuleUnavailableYearMonths(this);
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

    /// <summary>Field number for the "months" field.</summary>
    public const int MonthsFieldNumber = 2;

    private static readonly FieldCodec<int> _repeated_months_codec
        = pb::FieldCodec.ForInt32(18);

    private readonly pbc::RepeatedField<int> months_ = new();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<int> Months => months_;

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
        return Equals(other as AvailabilityRuleUnavailableYearMonths);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AvailabilityRuleUnavailableYearMonths other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (Type != other.Type) return false;
        if (!months_.Equals(other.months_)) return false;
        if (!Equals(Slot, other.Slot)) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (Type != AvailabilityRuleType.Compound) hash ^= Type.GetHashCode();
        hash ^= months_.GetHashCode();
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
      months_.WriteTo(output, _repeated_months_codec);
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

        months_.WriteTo(ref output, _repeated_months_codec);
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
        size += months_.CalculateSize(_repeated_months_codec);
        if (slot_ != null) size += 1 + pb::CodedOutputStream.ComputeMessageSize(Slot);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AvailabilityRuleUnavailableYearMonths other)
    {
        if (other == null) return;
        if (other.Type != AvailabilityRuleType.Compound) Type = other.Type;
        months_.Add(other.months_);
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
          case 18:
          case 16: {
            months_.AddEntriesFrom(input, _repeated_months_codec);
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
                case 18:
                case 16:
                {
                    months_.AddEntriesFrom(ref input, _repeated_months_codec);
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