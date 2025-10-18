using Google.Protobuf;
using pb = Google.Protobuf;
using pbc = Google.Protobuf.Collections;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

public sealed partial class GeneratorResponseDto : IMessage<GeneratorResponseDto>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly pb::MessageParser<GeneratorResponseDto> _parser = new pb::MessageParser<GeneratorResponseDto>(() => new GeneratorResponseDto());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<GeneratorResponseDto> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
        get { return global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratorResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
        get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GeneratorResponseDto() {
        OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GeneratorResponseDto(GeneratorResponseDto other) : this() {
        success_ = other.success_;
        message_ = other.message_;
        generatedTimetables_ = other.generatedTimetables_.Clone();
        date_ = other.date_;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GeneratorResponseDto Clone() {
        return new GeneratorResponseDto(this);
    }

    /// <summary>Field number for the "success" field.</summary>
    public const int SuccessFieldNumber = 1;
    private bool success_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Success {
        get { return success_; }
        set {
            success_ = value;
        }
    }

    /// <summary>Field number for the "message" field.</summary>
    public const int MessageFieldNumber = 2;
    private string message_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Message {
        get { return message_; }
        set {
            message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
        }
    }

    /// <summary>Field number for the "generated_timetables" field.</summary>
    public const int GeneratedTimetablesFieldNumber = 3;
    private static readonly pb::FieldCodec<global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratedTimetableDto> _repeated_generatedTimetables_codec
        = pb::FieldCodec.ForMessage(26, global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratedTimetableDto.Parser);
    private readonly pbc::RepeatedField<global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratedTimetableDto> generatedTimetables_ = new pbc::RepeatedField<global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratedTimetableDto>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.GeneratedTimetableDto> GeneratedTimetables {
        get { return generatedTimetables_; }
    }

    /// <summary>Field number for the "date" field.</summary>
    public const int DateFieldNumber = 4;
    private string date_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Date {
        get { return date_; }
        set {
            date_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
        return Equals(other as GeneratorResponseDto);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(GeneratorResponseDto other) {
        if (ReferenceEquals(other, null)) {
            return false;
        }
        if (ReferenceEquals(other, this)) {
            return true;
        }
        if (Success != other.Success) return false;
        if (Message != other.Message) return false;
        if(!generatedTimetables_.Equals(other.generatedTimetables_)) return false;
        if (Date != other.Date) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
        int hash = 1;
        if (Success != false) hash ^= Success.GetHashCode();
        if (Message.Length != 0) hash ^= Message.GetHashCode();
        hash ^= generatedTimetables_.GetHashCode();
        if (Date.Length != 0) hash ^= Date.GetHashCode();
        if (_unknownFields != null) {
            hash ^= _unknownFields.GetHashCode();
        }
        return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
        return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        output.WriteRawMessage(this);
#else
      if (Success != false) {
        output.WriteRawTag(8);
        output.WriteBool(Success);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Message);
      }
      generatedTimetables_.WriteTo(output, _repeated_generatedTimetables_codec);
      if (Date.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Date);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
#endif
    }

#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
        if (Success != false) {
            output.WriteRawTag(8);
            output.WriteBool(Success);
        }
        if (Message.Length != 0) {
            output.WriteRawTag(18);
            output.WriteString(Message);
        }
        generatedTimetables_.WriteTo(ref output, _repeated_generatedTimetables_codec);
        if (Date.Length != 0) {
            output.WriteRawTag(34);
            output.WriteString(Date);
        }
        if (_unknownFields != null) {
            _unknownFields.WriteTo(ref output);
        }
    }
#endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
        int size = 0;
        if (Success != false) {
            size += 1 + 1;
        }
        if (Message.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
        }
        size += generatedTimetables_.CalculateSize(_repeated_generatedTimetables_codec);
        if (Date.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Date);
        }
        if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
        }
        return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(GeneratorResponseDto other) {
        if (other == null) {
            return;
        }
        if (other.Success != false) {
            Success = other.Success;
        }
        if (other.Message.Length != 0) {
            Message = other.Message;
        }
        generatedTimetables_.Add(other.generatedTimetables_);
        if (other.Date.Length != 0) {
            Date = other.Date;
        }
        _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
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
            Success = input.ReadBool();
            break;
          }
          case 18: {
            Message = input.ReadString();
            break;
          }
          case 26: {
            generatedTimetables_.AddEntriesFrom(input, _repeated_generatedTimetables_codec);
            break;
          }
          case 34: {
            Date = input.ReadString();
            break;
          }
        }
      }
#endif
    }

#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
        uint tag;
        while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
                default:
                    _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                    break;
                case 8: {
                    Success = input.ReadBool();
                    break;
                }
                case 18: {
                    Message = input.ReadString();
                    break;
                }
                case 26: {
                    generatedTimetables_.AddEntriesFrom(ref input, _repeated_generatedTimetables_codec);
                    break;
                }
                case 34: {
                    Date = input.ReadString();
                    break;
                }
            }
        }
    }
#endif

}