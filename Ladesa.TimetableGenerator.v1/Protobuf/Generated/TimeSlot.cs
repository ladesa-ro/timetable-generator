using Google.Protobuf;
using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class TimeSlot : IMessage<TimeSlot>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<TimeSlot> _parser = new(() => new TimeSlot());
    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<TimeSlot> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => TimeSlotReflection.Descriptor.MessageTypes[0];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimeSlot()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimeSlot(TimeSlot other) : this()
    {
        start_ = other.start_;
        end_ = other.end_;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimeSlot Clone()
    {
        return new TimeSlot(this);
    }

    /// <summary>Field number for the "start" field.</summary>
    public const int StartFieldNumber = 1;

    private string start_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Start
    {
        get => start_;
        set => start_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "end" field.</summary>
    public const int EndFieldNumber = 2;

    private string end_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string End
    {
        get => end_;
        set => end_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as TimeSlot);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(TimeSlot other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (Start != other.Start) return false;
        if (End != other.End) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (Start.Length != 0) hash ^= Start.GetHashCode();
        if (End.Length != 0) hash ^= End.GetHashCode();
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
      if (Start.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Start);
      }
      if (End.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(End);
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
        if (Start.Length != 0)
        {
            output.WriteRawTag(10);
            output.WriteString(Start);
        }

        if (End.Length != 0)
        {
            output.WriteRawTag(18);
            output.WriteString(End);
        }

        if (_unknownFields != null) _unknownFields.WriteTo(ref output);
    }
#endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize()
    {
        var size = 0;
        if (Start.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(Start);
        if (End.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(End);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(TimeSlot other)
    {
        if (other == null) return;
        if (other.Start.Length != 0) Start = other.Start;
        if (other.End.Length != 0) End = other.End;
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
          case 10: {
            Start = input.ReadString();
            break;
          }
          case 18: {
            End = input.ReadString();
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
                case 10:
                {
                    Start = input.ReadString();
                    break;
                }
                case 18:
                {
                    End = input.ReadString();
                    break;
                }
            }
    }
#endif
}