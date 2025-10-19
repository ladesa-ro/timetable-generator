using Google.Protobuf;
using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class ServiceGenerateResponseDto : IMessage<ServiceGenerateResponseDto>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<ServiceGenerateResponseDto> _parser = new(() =>
        new ServiceGenerateResponseDto());

    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<ServiceGenerateResponseDto> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => ServiceGenerateResponseReflection.Descriptor.MessageTypes[0];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateResponseDto()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateResponseDto(ServiceGenerateResponseDto other) : this()
    {
        requestId_ = other.requestId_;
        isSuccessful_ = other.isSuccessful_;
        dateTimeIssued_ = other.dateTimeIssued_;
        switch (other.ResultCase)
        {
            case ResultOneofCase.ResultSuccess:
                ResultSuccess = other.ResultSuccess.Clone();
                break;
            case ResultOneofCase.ResultError:
                ResultError = other.ResultError.Clone();
                break;
        }

        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateResponseDto Clone()
    {
        return new ServiceGenerateResponseDto(this);
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

    /// <summary>Field number for the "is_successful" field.</summary>
    public const int IsSuccessfulFieldNumber = 2;

    private bool isSuccessful_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsSuccessful
    {
        get => isSuccessful_;
        set => isSuccessful_ = value;
    }

    /// <summary>Field number for the "result_success" field.</summary>
    public const int ResultSuccessFieldNumber = 3;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateResponseResultSuccessDto ResultSuccess
    {
        get => resultCase_ == ResultOneofCase.ResultSuccess ? (ServiceGenerateResponseResultSuccessDto)result_ : null;
        set
        {
            result_ = value;
            resultCase_ = value == null ? ResultOneofCase.None : ResultOneofCase.ResultSuccess;
        }
    }

    /// <summary>Field number for the "result_error" field.</summary>
    public const int ResultErrorFieldNumber = 4;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ServiceGenerateResponseResultErrorDto ResultError
    {
        get => resultCase_ == ResultOneofCase.ResultError ? (ServiceGenerateResponseResultErrorDto)result_ : null;
        set
        {
            result_ = value;
            resultCase_ = value == null ? ResultOneofCase.None : ResultOneofCase.ResultError;
        }
    }

    /// <summary>Field number for the "date_time_issued" field.</summary>
    public const int DateTimeIssuedFieldNumber = 5;

    private string dateTimeIssued_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DateTimeIssued
    {
        get => dateTimeIssued_;
        set => dateTimeIssued_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    private object result_;

    /// <summary>Enum of possible cases for the "result" oneof.</summary>
    public enum ResultOneofCase
    {
        None = 0,
        ResultSuccess = 3,
        ResultError = 4
    }

    private ResultOneofCase resultCase_ = ResultOneofCase.None;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ResultOneofCase ResultCase => resultCase_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearResult()
    {
        resultCase_ = ResultOneofCase.None;
        result_ = null;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as ServiceGenerateResponseDto);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ServiceGenerateResponseDto other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (RequestId != other.RequestId) return false;
        if (IsSuccessful != other.IsSuccessful) return false;
        if (!Equals(ResultSuccess, other.ResultSuccess)) return false;
        if (!Equals(ResultError, other.ResultError)) return false;
        if (DateTimeIssued != other.DateTimeIssued) return false;
        if (ResultCase != other.ResultCase) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (RequestId.Length != 0) hash ^= RequestId.GetHashCode();
        if (IsSuccessful != false) hash ^= IsSuccessful.GetHashCode();
        if (resultCase_ == ResultOneofCase.ResultSuccess) hash ^= ResultSuccess.GetHashCode();
        if (resultCase_ == ResultOneofCase.ResultError) hash ^= ResultError.GetHashCode();
        if (DateTimeIssued.Length != 0) hash ^= DateTimeIssued.GetHashCode();
        hash ^= (int)resultCase_;
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
      if (IsSuccessful != false) {
        output.WriteRawTag(16);
        output.WriteBool(IsSuccessful);
      }
      if (resultCase_ == ResultOneofCase.ResultSuccess) {
        output.WriteRawTag(26);
        output.WriteMessage(ResultSuccess);
      }
      if (resultCase_ == ResultOneofCase.ResultError) {
        output.WriteRawTag(34);
        output.WriteMessage(ResultError);
      }
      if (DateTimeIssued.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(DateTimeIssued);
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

        if (IsSuccessful != false)
        {
            output.WriteRawTag(16);
            output.WriteBool(IsSuccessful);
        }

        if (resultCase_ == ResultOneofCase.ResultSuccess)
        {
            output.WriteRawTag(26);
            output.WriteMessage(ResultSuccess);
        }

        if (resultCase_ == ResultOneofCase.ResultError)
        {
            output.WriteRawTag(34);
            output.WriteMessage(ResultError);
        }

        if (DateTimeIssued.Length != 0)
        {
            output.WriteRawTag(42);
            output.WriteString(DateTimeIssued);
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
        if (IsSuccessful != false) size += 1 + 1;
        if (resultCase_ == ResultOneofCase.ResultSuccess)
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(ResultSuccess);
        if (resultCase_ == ResultOneofCase.ResultError)
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(ResultError);
        if (DateTimeIssued.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(DateTimeIssued);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ServiceGenerateResponseDto other)
    {
        if (other == null) return;
        if (other.RequestId.Length != 0) RequestId = other.RequestId;
        if (other.IsSuccessful != false) IsSuccessful = other.IsSuccessful;
        if (other.DateTimeIssued.Length != 0) DateTimeIssued = other.DateTimeIssued;
        switch (other.ResultCase)
        {
            case ResultOneofCase.ResultSuccess:
                if (ResultSuccess == null) ResultSuccess = new ServiceGenerateResponseResultSuccessDto();
                ResultSuccess.MergeFrom(other.ResultSuccess);
                break;
            case ResultOneofCase.ResultError:
                if (ResultError == null) ResultError = new ServiceGenerateResponseResultErrorDto();
                ResultError.MergeFrom(other.ResultError);
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
          case 10: {
            RequestId = input.ReadString();
            break;
          }
          case 16: {
            IsSuccessful = input.ReadBool();
            break;
          }
          case 26: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultSuccessDto subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultSuccessDto();
            if (resultCase_ == ResultOneofCase.ResultSuccess) {
              subBuilder.MergeFrom(ResultSuccess);
            }
            input.ReadMessage(subBuilder);
            ResultSuccess = subBuilder;
            break;
          }
          case 34: {
            global::Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultErrorDto subBuilder =
 new global::Ladesa.TimetableGenerator.v1.Protobuf.ServiceGenerateResponseResultErrorDto();
            if (resultCase_ == ResultOneofCase.ResultError) {
              subBuilder.MergeFrom(ResultError);
            }
            input.ReadMessage(subBuilder);
            ResultError = subBuilder;
            break;
          }
          case 42: {
            DateTimeIssued = input.ReadString();
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
                case 16:
                {
                    IsSuccessful = input.ReadBool();
                    break;
                }
                case 26:
                {
                    var subBuilder = new ServiceGenerateResponseResultSuccessDto();
                    if (resultCase_ == ResultOneofCase.ResultSuccess) subBuilder.MergeFrom(ResultSuccess);
                    input.ReadMessage(subBuilder);
                    ResultSuccess = subBuilder;
                    break;
                }
                case 34:
                {
                    var subBuilder = new ServiceGenerateResponseResultErrorDto();
                    if (resultCase_ == ResultOneofCase.ResultError) subBuilder.MergeFrom(ResultError);
                    input.ReadMessage(subBuilder);
                    ResultError = subBuilder;
                    break;
                }
                case 42:
                {
                    DateTimeIssued = input.ReadString();
                    break;
                }
            }
    }
#endif
}