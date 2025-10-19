using Google.Protobuf;
using pb = Google.Protobuf;
using pbc = Google.Protobuf.Collections;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.v1.Protobuf;

public sealed partial class TimetableGrid : IMessage<TimetableGrid>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly MessageParser<TimetableGrid> _parser = new(() => new TimetableGrid());
    private UnknownFieldSet _unknownFields;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static MessageParser<TimetableGrid> Parser => _parser;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor => TimetableGridReflection.Descriptor.MessageTypes[0];

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor IMessage.Descriptor => Descriptor;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimetableGrid()
    {
        OnConstruction();
    }

    partial void OnConstruction();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimetableGrid(TimetableGrid other) : this()
    {
        dateStart_ = other.dateStart_;
        dateEnd_ = other.dateEnd_;
        timeSlots_ = other.timeSlots_.Clone();
        schedules_ = other.schedules_.Clone();
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TimetableGrid Clone()
    {
        return new TimetableGrid(this);
    }

    /// <summary>Field number for the "date_start" field.</summary>
    public const int DateStartFieldNumber = 1;

    private string dateStart_ = "";

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

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DateEnd
    {
        get => dateEnd_;
        set => dateEnd_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }

    /// <summary>Field number for the "time_slots" field.</summary>
    public const int TimeSlotsFieldNumber = 3;

    private static readonly FieldCodec<TimeSlot> _repeated_timeSlots_codec
        = pb::FieldCodec.ForMessage(26, TimeSlot.Parser);

    private readonly pbc::RepeatedField<TimeSlot> timeSlots_ = new();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<TimeSlot> TimeSlots => timeSlots_;

    /// <summary>Field number for the "schedules" field.</summary>
    public const int SchedulesFieldNumber = 4;

    private static readonly FieldCodec<TimetableGridSchedule> _repeated_schedules_codec
        = pb::FieldCodec.ForMessage(34, TimetableGridSchedule.Parser);

    private readonly pbc::RepeatedField<TimetableGridSchedule> schedules_ = new();

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<TimetableGridSchedule> Schedules => schedules_;

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other)
    {
        return Equals(other as TimetableGrid);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(TimetableGrid other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (DateStart != other.DateStart) return false;
        if (DateEnd != other.DateEnd) return false;
        if (!timeSlots_.Equals(other.timeSlots_)) return false;
        if (!schedules_.Equals(other.schedules_)) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode()
    {
        var hash = 1;
        if (DateStart.Length != 0) hash ^= DateStart.GetHashCode();
        if (DateEnd.Length != 0) hash ^= DateEnd.GetHashCode();
        hash ^= timeSlots_.GetHashCode();
        hash ^= schedules_.GetHashCode();
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
      timeSlots_.WriteTo(output, _repeated_timeSlots_codec);
      schedules_.WriteTo(output, _repeated_schedules_codec);
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

        timeSlots_.WriteTo(ref output, _repeated_timeSlots_codec);
        schedules_.WriteTo(ref output, _repeated_schedules_codec);
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
        size += timeSlots_.CalculateSize(_repeated_timeSlots_codec);
        size += schedules_.CalculateSize(_repeated_schedules_codec);
        if (_unknownFields != null) size += _unknownFields.CalculateSize();
        return size;
    }

    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(TimetableGrid other)
    {
        if (other == null) return;
        if (other.DateStart.Length != 0) DateStart = other.DateStart;
        if (other.DateEnd.Length != 0) DateEnd = other.DateEnd;
        timeSlots_.Add(other.timeSlots_);
        schedules_.Add(other.schedules_);
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
            timeSlots_.AddEntriesFrom(input, _repeated_timeSlots_codec);
            break;
          }
          case 34: {
            schedules_.AddEntriesFrom(input, _repeated_schedules_codec);
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
                    timeSlots_.AddEntriesFrom(ref input, _repeated_timeSlots_codec);
                    break;
                }
                case 34:
                {
                    schedules_.AddEntriesFrom(ref input, _repeated_schedules_codec);
                    break;
                }
            }
    }
#endif
}