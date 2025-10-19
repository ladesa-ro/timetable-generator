using Google.Protobuf;
using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class ServiceGenerateRequestDto : IMessage<ServiceGenerateRequestDto>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<ServiceGenerateRequestDto> _parser =
        new(() => new ServiceGenerateRequestDto());

    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<ServiceGenerateRequestDto> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => ServiceGenerateRequestReflection.Descriptor.MessageTypes[0];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateRequestDto()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateRequestDto(ServiceGenerateRequestDto other) : this()
    {
        requestId_ = other.requestId_;
        generateRequest_ = other.generateRequest_ != null ? other.generateRequest_.Clone() : null;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateRequestDto Clone()
    {
        return new ServiceGenerateRequestDto(this);
    }

    /// <summary>Field number for the "request_id" field.</summary>
    public const int RequestIdFieldNumber = 1;

    private string requestId_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string RequestId
    {
        get => requestId_;
        set => requestId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "generate_request" field.</summary>
    public const int GenerateRequestFieldNumber = 2;

    private GenerateRequestDto generateRequest_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GenerateRequestDto GenerateRequest
    {
        get => generateRequest_;
        set => generateRequest_ = value;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as ServiceGenerateRequestDto);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ServiceGenerateRequestDto other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (RequestId != other.RequestId) return false;
        if (!Equals(GenerateRequest, other.GenerateRequest)) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (RequestId.Length != 0) hash ^= RequestId.GetHashCode();
        if (generateRequest_ != null) hash ^= GenerateRequest.GetHashCode();
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
      if (RequestId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(RequestId);
      }
      if (generateRequest_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(GenerateRequest);
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
        if (RequestId.Length != 0)
        {
            output.WriteRawTag(10);
            output.WriteString(RequestId);
        }

        if (generateRequest_ != null)
        {
            output.WriteRawTag(18);
            output.WriteMessage(GenerateRequest);
        }

        if (_unknownFields != null) _unknownFields.WriteTo(ref output);
    }
#endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize()
    {
        var size = 0;
        if (RequestId.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(RequestId);
        if (generateRequest_ != null) size += 1 + pb::CodedOutputStream.ComputeMessageSize(GenerateRequest);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ServiceGenerateRequestDto other)
    {
        if (other == null) return;
        if (other.RequestId.Length != 0) RequestId = other.RequestId;
        if (other.generateRequest_ != null)
        {
            if (generateRequest_ == null) GenerateRequest = new GenerateRequestDto();
            GenerateRequest.MergeFrom(other.GenerateRequest);
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
          case 10: {
            RequestId = input.ReadString();
            break;
          }
          case 18: {
            if (generateRequest_ == null) {
              GenerateRequest = new global::Ladesa.TimetableGenerator.v1.Protobuf.GenerateRequestDto();
            }
            input.ReadMessage(GenerateRequest);
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
                    RequestId = input.ReadString();
                    break;
                }
                case 18:
                {
                    if (generateRequest_ == null) GenerateRequest = new GenerateRequestDto();
                    input.ReadMessage(GenerateRequest);
                    break;
                }
            }
    }
#endif
}