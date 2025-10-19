using Google.Protobuf;
using pb = Google.Protobuf;
using pbc = Google.Protobuf.Collections;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class GenerateRequestDto : IMessage<GenerateRequestDto>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<GenerateRequestDto> _parser = new(() => new GenerateRequestDto());
    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<GenerateRequestDto> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => GenerateRequestReflection.Descriptor.MessageTypes[0];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GenerateRequestDto()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GenerateRequestDto(GenerateRequestDto other) : this()
    {
        dateStart_ = other.dateStart_;
        dateEnd_ = other.dateEnd_;
        groups_ = other.groups_.Clone();
        teachers_ = other.teachers_.Clone();
        diarys_ = other.diarys_.Clone();
        timeSlots_ = other.timeSlots_.Clone();
        previousTimetableGrid_ = other.previousTimetableGrid_ != null ? other.previousTimetableGrid_.Clone() : null;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GenerateRequestDto Clone()
    {
        return new GenerateRequestDto(this);
    }

    /// <summary>Field number for the "date_start" field.</summary>
    public const int DateStartFieldNumber = 1;

    private string dateStart_ = "";

    /// <summary>
    /// date-time
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DateStart
    {
        get => dateStart_;
        set => dateStart_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "date_end" field.</summary>
    public const int DateEndFieldNumber = 2;

    private string dateEnd_ = "";

    /// <summary>
    /// date-time
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DateEnd
    {
        get => dateEnd_;
        set => dateEnd_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "groups" field.</summary>
    public const int GroupsFieldNumber = 3;

    private static readonly FieldCodec<Group> _repeated_groups_codec
        = pb::FieldCodec.ForMessage(26, Group.Parser);

    private readonly pbc::RepeatedField<Group> groups_ = new();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<Group> Groups => groups_;

    /// <summary>Field number for the "teachers" field.</summary>
    public const int TeachersFieldNumber = 4;

    private static readonly FieldCodec<Teacher> _repeated_teachers_codec
        = pb::FieldCodec.ForMessage(34, Teacher.Parser);

    private readonly pbc::RepeatedField<Teacher> teachers_ = new();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<Teacher> Teachers => teachers_;

    /// <summary>Field number for the "diarys" field.</summary>
    public const int DiarysFieldNumber = 5;

    private static readonly FieldCodec<Diary> _repeated_diarys_codec
        = pb::FieldCodec.ForMessage(42, Diary.Parser);

    private readonly pbc::RepeatedField<Diary> diarys_ = new();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<Diary> Diarys => diarys_;

    /// <summary>Field number for the "time_slots" field.</summary>
    public const int TimeSlotsFieldNumber = 6;

    private static readonly FieldCodec<TimeSlot> _repeated_timeSlots_codec
        = pb::FieldCodec.ForMessage(50, TimeSlot.Parser);

    private readonly pbc::RepeatedField<TimeSlot> timeSlots_ = new();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<TimeSlot> TimeSlots => timeSlots_;

    /// <summary>Field number for the "previous_timetable_grid" field.</summary>
    public const int PreviousTimetableGridFieldNumber = 7;

    private TimetableGrid previousTimetableGrid_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimetableGrid PreviousTimetableGrid
    {
        get => previousTimetableGrid_;
        set => previousTimetableGrid_ = value;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as GenerateRequestDto);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(GenerateRequestDto other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (DateStart != other.DateStart) return false;
        if (DateEnd != other.DateEnd) return false;
        if (!groups_.Equals(other.groups_)) return false;
        if (!teachers_.Equals(other.teachers_)) return false;
        if (!diarys_.Equals(other.diarys_)) return false;
        if (!timeSlots_.Equals(other.timeSlots_)) return false;
        if (!Equals(PreviousTimetableGrid, other.PreviousTimetableGrid)) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (DateStart.Length != 0) hash ^= DateStart.GetHashCode();
        if (DateEnd.Length != 0) hash ^= DateEnd.GetHashCode();
        hash ^= groups_.GetHashCode();
        hash ^= teachers_.GetHashCode();
        hash ^= diarys_.GetHashCode();
        hash ^= timeSlots_.GetHashCode();
        if (previousTimetableGrid_ != null) hash ^= PreviousTimetableGrid.GetHashCode();
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
      if (DateStart.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(DateStart);
      }
      if (DateEnd.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(DateEnd);
      }
      groups_.WriteTo(output, _repeated_groups_codec);
      teachers_.WriteTo(output, _repeated_teachers_codec);
      diarys_.WriteTo(output, _repeated_diarys_codec);
      timeSlots_.WriteTo(output, _repeated_timeSlots_codec);
      if (previousTimetableGrid_ != null) {
        output.WriteRawTag(58);
        output.WriteMessage(PreviousTimetableGrid);
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
        if (DateStart.Length != 0)
        {
            output.WriteRawTag(10);
            output.WriteString(DateStart);
        }

        if (DateEnd.Length != 0)
        {
            output.WriteRawTag(18);
            output.WriteString(DateEnd);
        }

        groups_.WriteTo(ref output, _repeated_groups_codec);
        teachers_.WriteTo(ref output, _repeated_teachers_codec);
        diarys_.WriteTo(ref output, _repeated_diarys_codec);
        timeSlots_.WriteTo(ref output, _repeated_timeSlots_codec);
        if (previousTimetableGrid_ != null)
        {
            output.WriteRawTag(58);
            output.WriteMessage(PreviousTimetableGrid);
        }

        if (_unknownFields != null) _unknownFields.WriteTo(ref output);
    }
#endif

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize()
    {
        var size = 0;
        if (DateStart.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(DateStart);
        if (DateEnd.Length != 0) size += 1 + pb::CodedOutputStream.ComputeStringSize(DateEnd);
        size += groups_.CalculateSize(_repeated_groups_codec);
        size += teachers_.CalculateSize(_repeated_teachers_codec);
        size += diarys_.CalculateSize(_repeated_diarys_codec);
        size += timeSlots_.CalculateSize(_repeated_timeSlots_codec);
        if (previousTimetableGrid_ != null) size += 1 + pb::CodedOutputStream.ComputeMessageSize(PreviousTimetableGrid);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(GenerateRequestDto other)
    {
        if (other == null) return;
        if (other.DateStart.Length != 0) DateStart = other.DateStart;
        if (other.DateEnd.Length != 0) DateEnd = other.DateEnd;
        groups_.Add(other.groups_);
        teachers_.Add(other.teachers_);
        diarys_.Add(other.diarys_);
        timeSlots_.Add(other.timeSlots_);
        if (other.previousTimetableGrid_ != null)
        {
            if (previousTimetableGrid_ == null) PreviousTimetableGrid = new TimetableGrid();
            PreviousTimetableGrid.MergeFrom(other.PreviousTimetableGrid);
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
            DateStart = input.ReadString();
            break;
          }
          case 18: {
            DateEnd = input.ReadString();
            break;
          }
          case 26: {
            groups_.AddEntriesFrom(input, _repeated_groups_codec);
            break;
          }
          case 34: {
            teachers_.AddEntriesFrom(input, _repeated_teachers_codec);
            break;
          }
          case 42: {
            diarys_.AddEntriesFrom(input, _repeated_diarys_codec);
            break;
          }
          case 50: {
            timeSlots_.AddEntriesFrom(input, _repeated_timeSlots_codec);
            break;
          }
          case 58: {
            if (previousTimetableGrid_ == null) {
              PreviousTimetableGrid = new global::Ladesa.TimetableGenerator.v1.Protobuf.TimetableGrid();
            }
            input.ReadMessage(PreviousTimetableGrid);
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
                    DateStart = input.ReadString();
                    break;
                }
                case 18:
                {
                    DateEnd = input.ReadString();
                    break;
                }
                case 26:
                {
                    groups_.AddEntriesFrom(ref input, _repeated_groups_codec);
                    break;
                }
                case 34:
                {
                    teachers_.AddEntriesFrom(ref input, _repeated_teachers_codec);
                    break;
                }
                case 42:
                {
                    diarys_.AddEntriesFrom(ref input, _repeated_diarys_codec);
                    break;
                }
                case 50:
                {
                    timeSlots_.AddEntriesFrom(ref input, _repeated_timeSlots_codec);
                    break;
                }
                case 58:
                {
                    if (previousTimetableGrid_ == null) PreviousTimetableGrid = new TimetableGrid();
                    input.ReadMessage(PreviousTimetableGrid);
                    break;
                }
            }
    }
#endif
}