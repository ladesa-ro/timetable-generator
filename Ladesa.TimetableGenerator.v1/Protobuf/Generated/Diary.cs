using Google.Protobuf;
using pb = Google.Protobuf;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class Diary : IMessage<Diary>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<Diary> _parser = new(() => new Diary());
    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<Diary> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => DiaryReflection.Descriptor.MessageTypes[0];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Diary()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Diary(Diary other) : this()
    {
        id_ = other.id_;
        groupId_ = other.groupId_;
        teacherId_ = other.teacherId_;
        subjectId_ = other.subjectId_;
        weekLimit_ = other.weekLimit_;
        remaining_ = other.remaining_;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Diary Clone()
    {
        return new Diary(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;

    private string id_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Id
    {
        get => id_;
        set => id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "group_id" field.</summary>
    public const int GroupIdFieldNumber = 2;

    private string groupId_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string GroupId
    {
        get => groupId_;
        set => groupId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
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

    /// <summary>Field number for the "subject_id" field.</summary>
    public const int SubjectIdFieldNumber = 4;

    private string subjectId_ = "";

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SubjectId
    {
        get => subjectId_;
        set => subjectId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "week_limit" field.</summary>
    public const int WeekLimitFieldNumber = 5;

    private int weekLimit_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int WeekLimit
    {
        get => weekLimit_;
        set => weekLimit_ = value;
    }

    /// <summary>Field number for the "remaining" field.</summary>
    public const int RemainingFieldNumber = 6;

    private int remaining_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Remaining
    {
        get => remaining_;
        set => remaining_ = value;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as Diary);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Diary other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (Id != other.Id) return false;
        if (GroupId != other.GroupId) return false;
        if (TeacherId != other.TeacherId) return false;
        if (SubjectId != other.SubjectId) return false;
        if (WeekLimit != other.WeekLimit) return false;
        if (Remaining != other.Remaining) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (Id.Length != 0) hash ^= Id.GetHashCode();
        if (GroupId.Length != 0) hash ^= GroupId.GetHashCode();
        if (TeacherId.Length != 0) hash ^= TeacherId.GetHashCode();
        if (SubjectId.Length != 0) hash ^= SubjectId.GetHashCode();
        if (WeekLimit != 0) hash ^= WeekLimit.GetHashCode();
        if (Remaining != 0) hash ^= Remaining.GetHashCode();
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
      if (Id.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Id);
      }
      if (GroupId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(GroupId);
      }
      if (TeacherId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(TeacherId);
      }
      if (SubjectId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(SubjectId);
      }
      if (WeekLimit != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(WeekLimit);
      }
      if (Remaining != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(Remaining);
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
        if (Id.Length != 0)
        {
            output.WriteRawTag(10);
            output.WriteString(Id);
        }

        if (GroupId.Length != 0)
        {
            output.WriteRawTag(18);
            output.WriteString(GroupId);
        }

        if (TeacherId.Length != 0)
        {
            output.WriteRawTag(26);
            output.WriteString(TeacherId);
        }

        if (SubjectId.Length != 0)
        {
            output.WriteRawTag(34);
            output.WriteString(SubjectId);
        }

        if (WeekLimit != 0)
        {
            output.WriteRawTag(40);
            output.WriteInt32(WeekLimit);
        }

        if (Remaining != 0)
        {
            output.WriteRawTag(48);
            output.WriteInt32(Remaining);
        }

        if (_unknownFields != null) _unknownFields.WriteTo(ref output);
    }
#endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize()
    {
        var size = 0;
        if (Id.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
        if (GroupId.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(GroupId);
        if (TeacherId.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(TeacherId);
        if (SubjectId.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(SubjectId);
        if (WeekLimit != 0) size += 1 + pb::CodedOutputStream.ComputeInt32Size(WeekLimit);
        if (Remaining != 0) size += 1 + pb::CodedOutputStream.ComputeInt32Size(Remaining);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Diary other)
    {
        if (other == null) return;
        if (other.Id.Length != 0) Id = other.Id;
        if (other.GroupId.Length != 0) GroupId = other.GroupId;
        if (other.TeacherId.Length != 0) TeacherId = other.TeacherId;
        if (other.SubjectId.Length != 0) SubjectId = other.SubjectId;
        if (other.WeekLimit != 0) WeekLimit = other.WeekLimit;
        if (other.Remaining != 0) Remaining = other.Remaining;
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
            Id = input.ReadString();
            break;
          }
          case 18: {
            GroupId = input.ReadString();
            break;
          }
          case 26: {
            TeacherId = input.ReadString();
            break;
          }
          case 34: {
            SubjectId = input.ReadString();
            break;
          }
          case 40: {
            WeekLimit = input.ReadInt32();
            break;
          }
          case 48: {
            Remaining = input.ReadInt32();
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
                    Id = input.ReadString();
                    break;
                }
                case 18:
                {
                    GroupId = input.ReadString();
                    break;
                }
                case 26:
                {
                    TeacherId = input.ReadString();
                    break;
                }
                case 34:
                {
                    SubjectId = input.ReadString();
                    break;
                }
                case 40:
                {
                    WeekLimit = input.ReadInt32();
                    break;
                }
                case 48:
                {
                    Remaining = input.ReadInt32();
                    break;
                }
            }
    }
#endif
}