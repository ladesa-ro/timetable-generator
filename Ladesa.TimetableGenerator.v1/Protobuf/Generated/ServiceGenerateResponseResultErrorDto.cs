using Google.Protobuf;
using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class ServiceGenerateResponseResultErrorDto : IMessage<ServiceGenerateResponseResultErrorDto>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<ServiceGenerateResponseResultErrorDto> _parser = new(() =>
        new ServiceGenerateResponseResultErrorDto());

    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<ServiceGenerateResponseResultErrorDto> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => ServiceGenerateResponseReflection.Descriptor.MessageTypes[2];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateResponseResultErrorDto()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateResponseResultErrorDto(ServiceGenerateResponseResultErrorDto other) : this()
    {
        errorCode_ = other.errorCode_;
        errorMessage_ = other.errorMessage_;
        additionalInfo_ = other.additionalInfo_;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateResponseResultErrorDto Clone()
    {
        return new ServiceGenerateResponseResultErrorDto(this);
    }

    /// <summary>Field number for the "error_code" field.</summary>
    public const int ErrorCodeFieldNumber = 1;

    private string errorCode_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ErrorCode
    {
        get => errorCode_;
        set => errorCode_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "error_message" field.</summary>
    public const int ErrorMessageFieldNumber = 2;

    private string errorMessage_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ErrorMessage
    {
        get => errorMessage_;
        set => errorMessage_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "additional_info" field.</summary>
    public const int AdditionalInfoFieldNumber = 3;

    private string additionalInfo_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string AdditionalInfo
    {
        get => additionalInfo_ ?? "";
        set => additionalInfo_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Gets whether the "additional_info" field is set</summary>
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasAdditionalInfo => additionalInfo_ != null;

    /// <summary>Clears the value of the "additional_info" field</summary>
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearAdditionalInfo()
    {
        additionalInfo_ = null;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as ServiceGenerateResponseResultErrorDto);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ServiceGenerateResponseResultErrorDto other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (ErrorCode != other.ErrorCode) return false;
        if (ErrorMessage != other.ErrorMessage) return false;
        if (AdditionalInfo != other.AdditionalInfo) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (ErrorCode.Length != 0) hash ^= ErrorCode.GetHashCode();
        if (ErrorMessage.Length != 0) hash ^= ErrorMessage.GetHashCode();
        if (HasAdditionalInfo) hash ^= AdditionalInfo.GetHashCode();
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
      if (ErrorCode.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ErrorCode);
      }
      if (ErrorMessage.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ErrorMessage);
      }
      if (HasAdditionalInfo) {
        output.WriteRawTag(26);
        output.WriteString(AdditionalInfo);
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
        if (ErrorCode.Length != 0)
        {
            output.WriteRawTag(10);
            output.WriteString(ErrorCode);
        }

        if (ErrorMessage.Length != 0)
        {
            output.WriteRawTag(18);
            output.WriteString(ErrorMessage);
        }

        if (HasAdditionalInfo)
        {
            output.WriteRawTag(26);
            output.WriteString(AdditionalInfo);
        }

        if (_unknownFields != null) _unknownFields.WriteTo(ref output);
    }
#endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize()
    {
        var size = 0;
        if (ErrorCode.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(ErrorCode);
        if (ErrorMessage.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(ErrorMessage);
        if (HasAdditionalInfo) size += 1 + pb::CodedOutputStream.ComputeStringSize(AdditionalInfo);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ServiceGenerateResponseResultErrorDto other)
    {
        if (other == null) return;
        if (other.ErrorCode.Length != 0) ErrorCode = other.ErrorCode;
        if (other.ErrorMessage.Length != 0) ErrorMessage = other.ErrorMessage;
        if (other.HasAdditionalInfo) AdditionalInfo = other.AdditionalInfo;
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
            ErrorCode = input.ReadString();
            break;
          }
          case 18: {
            ErrorMessage = input.ReadString();
            break;
          }
          case 26: {
            AdditionalInfo = input.ReadString();
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
                    ErrorCode = input.ReadString();
                    break;
                }
                case 18:
                {
                    ErrorMessage = input.ReadString();
                    break;
                }
                case 26:
                {
                    AdditionalInfo = input.ReadString();
                    break;
                }
            }
    }
#endif
}