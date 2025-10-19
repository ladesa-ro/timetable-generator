using Google.Protobuf;
using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class TimetableGridSchedule : IMessage<TimetableGridSchedule>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<TimetableGridSchedule> _parser = new(() => new TimetableGridSchedule());
    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<TimetableGridSchedule> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => TimetableGridScheduleReflection.Descriptor.MessageTypes[0];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimetableGridSchedule()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimetableGridSchedule(TimetableGridSchedule other) : this()
    {
        date_ = other.date_;
        diaryId_ = other.diaryId_;
        teacherId_ = other.teacherId_;
        groupId_ = other.groupId_;
        timeSlot_ = other.timeSlot_ != null ? other.timeSlot_.Clone() : null;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimetableGridSchedule Clone()
    {
        return new TimetableGridSchedule(this);
    }

    /// <summary>Field number for the "date" field.</summary>
    public const int DateFieldNumber = 1;

    private string date_ = "";

    /// <summary>
    /// date-time
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Date
    {
        get => date_;
        set => date_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "diary_id" field.</summary>
    public const int DiaryIdFieldNumber = 2;

    private string diaryId_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DiaryId
    {
        get => diaryId_;
        set => diaryId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "teacher_id" field.</summary>
    public const int TeacherIdFieldNumber = 3;

    private string teacherId_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string TeacherId
    {
        get => teacherId_;
        set => teacherId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "group_id" field.</summary>
    public const int GroupIdFieldNumber = 4;

    private string groupId_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string GroupId
    {
        get => groupId_;
        set => groupId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "time_slot" field.</summary>
    public const int TimeSlotFieldNumber = 5;

    private TimeSlot timeSlot_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimeSlot TimeSlot
    {
        get => timeSlot_;
        set => timeSlot_ = value;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as TimetableGridSchedule);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(TimetableGridSchedule other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (Date != other.Date) return false;
        if (DiaryId != other.DiaryId) return false;
        if (TeacherId != other.TeacherId) return false;
        if (GroupId != other.GroupId) return false;
        if (!Equals(TimeSlot, other.TimeSlot)) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (Date.Length != 0) hash ^= Date.GetHashCode();
        if (DiaryId.Length != 0) hash ^= DiaryId.GetHashCode();
        if (TeacherId.Length != 0) hash ^= TeacherId.GetHashCode();
        if (GroupId.Length != 0) hash ^= GroupId.GetHashCode();
        if (timeSlot_ != null) hash ^= TimeSlot.GetHashCode();
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
      if (Date.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Date);
      }
      if (DiaryId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(DiaryId);
      }
      if (TeacherId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(TeacherId);
      }
      if (GroupId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(GroupId);
      }
      if (timeSlot_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(TimeSlot);
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
        if (Date.Length != 0)
        {
            output.WriteRawTag(10);
            output.WriteString(Date);
        }

        if (DiaryId.Length != 0)
        {
            output.WriteRawTag(18);
            output.WriteString(DiaryId);
        }

        if (TeacherId.Length != 0)
        {
            output.WriteRawTag(26);
            output.WriteString(TeacherId);
        }

        if (GroupId.Length != 0)
        {
            output.WriteRawTag(34);
            output.WriteString(GroupId);
        }

        if (timeSlot_ != null)
        {
            output.WriteRawTag(42);
            output.WriteMessage(TimeSlot);
        }

        if (_unknownFields != null) _unknownFields.WriteTo(ref output);
    }
#endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize()
    {
        var size = 0;
        if (Date.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(Date);
        if (DiaryId.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(DiaryId);
        if (TeacherId.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(TeacherId);
        if (GroupId.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(GroupId);
        if (timeSlot_ != null) size += 1 + pb::CodedOutputStream.ComputeMessageSize(TimeSlot);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(TimetableGridSchedule other)
    {
        if (other == null) return;
        if (other.Date.Length != 0) Date = other.Date;
        if (other.DiaryId.Length != 0) DiaryId = other.DiaryId;
        if (other.TeacherId.Length != 0) TeacherId = other.TeacherId;
        if (other.GroupId.Length != 0) GroupId = other.GroupId;
        if (other.timeSlot_ != null)
        {
            if (timeSlot_ == null) TimeSlot = new TimeSlot();
            TimeSlot.MergeFrom(other.TimeSlot);
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
            Date = input.ReadString();
            break;
          }
          case 18: {
            DiaryId = input.ReadString();
            break;
          }
          case 26: {
            TeacherId = input.ReadString();
            break;
          }
          case 34: {
            GroupId = input.ReadString();
            break;
          }
          case 42: {
            if (timeSlot_ == null) {
              TimeSlot = new global::Ladesa.TimetableGenerator.v1.Protobuf.TimeSlot();
            }
            input.ReadMessage(TimeSlot);
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
                    Date = input.ReadString();
                    break;
                }
                case 18:
                {
                    DiaryId = input.ReadString();
                    break;
                }
                case 26:
                {
                    TeacherId = input.ReadString();
                    break;
                }
                case 34:
                {
                    GroupId = input.ReadString();
                    break;
                }
                case 42:
                {
                    if (timeSlot_ == null) TimeSlot = new TimeSlot();
                    input.ReadMessage(TimeSlot);
                    break;
                }
            }
    }
#endif
}