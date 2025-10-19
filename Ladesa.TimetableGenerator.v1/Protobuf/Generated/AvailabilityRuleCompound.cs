using Google.Protobuf;
using pb = Google.Protobuf;
using pbc = Google.Protobuf.Collections;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class AvailabilityRuleCompound : IMessage<AvailabilityRuleCompound>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<AvailabilityRuleCompound> _parser = new(() => new AvailabilityRuleCompound());
    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<AvailabilityRuleCompound> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => AvailabilityRuleReflection.Descriptor.MessageTypes[1];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleCompound()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleCompound(AvailabilityRuleCompound other) : this()
    {
        type_ = other.type_;
        rules_ = other.rules_.Clone();
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityRuleCompound Clone()
    {
        return new AvailabilityRuleCompound(this);
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

    /// <summary>Field number for the "rules" field.</summary>
    public const int RulesFieldNumber = 2;

    private static readonly FieldCodec<AvailabilityRule> _repeated_rules_codec
        = pb::FieldCodec.ForMessage(18, AvailabilityRule.Parser);

    private readonly pbc::RepeatedField<AvailabilityRule> rules_ = new();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<AvailabilityRule> Rules => rules_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as AvailabilityRuleCompound);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AvailabilityRuleCompound other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (Type != other.Type) return false;
        if (!rules_.Equals(other.rules_)) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (Type != AvailabilityRuleType.Compound) hash ^= Type.GetHashCode();
        hash ^= rules_.GetHashCode();
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
      rules_.WriteTo(output, _repeated_rules_codec);
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

        rules_.WriteTo(ref output, _repeated_rules_codec);
        if (_unknownFields != null) _unknownFields.WriteTo(ref output);
    }
#endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize()
    {
        var size = 0;
        if (Type != AvailabilityRuleType.Compound) size += 1 + pb::CodedOutputStream.ComputeEnumSize((int)Type);
        size += rules_.CalculateSize(_repeated_rules_codec);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AvailabilityRuleCompound other)
    {
        if (other == null) return;
        if (other.Type != AvailabilityRuleType.Compound) Type = other.Type;
        rules_.Add(other.rules_);
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
            rules_.AddEntriesFrom(input, _repeated_rules_codec);
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
                    rules_.AddEntriesFrom(ref input, _repeated_rules_codec);
                    break;
                }
            }
    }
#endif
}