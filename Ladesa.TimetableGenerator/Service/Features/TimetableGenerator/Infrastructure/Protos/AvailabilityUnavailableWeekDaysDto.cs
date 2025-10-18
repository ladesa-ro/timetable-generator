using Google.Protobuf;
using pb = Google.Protobuf;
using pbc = Google.Protobuf.Collections;
using pbr = Google.Protobuf.Reflection;

namespace Ladesa.TimetableGenerator.Service.Infrastructure.Protos;

public sealed partial class AvailabilityUnavailableWeekDaysDto : IMessage<AvailabilityUnavailableWeekDaysDto>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , IBufferMessage
#endif
{
    private static readonly pb::MessageParser<AvailabilityUnavailableWeekDaysDto> _parser = new pb::MessageParser<AvailabilityUnavailableWeekDaysDto>(() => new AvailabilityUnavailableWeekDaysDto());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<AvailabilityUnavailableWeekDaysDto> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
        get { return global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityDtoReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
        get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityUnavailableWeekDaysDto() {
        OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityUnavailableWeekDaysDto(AvailabilityUnavailableWeekDaysDto other) : this() {
        type_ = other.type_;
        weekDays_ = other.weekDays_.Clone();
        slot_ = other.slot_ != null ? other.slot_.Clone() : null;
        _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AvailabilityUnavailableWeekDaysDto Clone() {
        return new AvailabilityUnavailableWeekDaysDto(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;
    private global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto type_ = global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto.Compound;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto Type {
        get { return type_; }
        set {
            type_ = value;
        }
    }

    /// <summary>Field number for the "week_days" field.</summary>
    public const int WeekDaysFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.WeekDayDto> _repeated_weekDays_codec
        = pb::FieldCodec.ForEnum(18, x => (int) x, x => (global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.WeekDayDto) x);
    private readonly pbc::RepeatedField<global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.WeekDayDto> weekDays_ = new pbc::RepeatedField<global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.WeekDayDto>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.WeekDayDto> WeekDays {
        get { return weekDays_; }
    }

    /// <summary>Field number for the "slot" field.</summary>
    public const int SlotFieldNumber = 3;
    private global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TimeSlotDto slot_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TimeSlotDto Slot {
        get { return slot_; }
        set {
            slot_ = value;
        }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
        return Equals(other as AvailabilityUnavailableWeekDaysDto);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AvailabilityUnavailableWeekDaysDto other) {
        if (ReferenceEquals(other, null)) {
            return false;
        }
        if (ReferenceEquals(other, this)) {
            return true;
        }
        if (Type != other.Type) return false;
        if(!weekDays_.Equals(other.weekDays_)) return false;
        if (!object.Equals(Slot, other.Slot)) return false;
        return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
        int hash = 1;
        if (Type != global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto.Compound) hash ^= Type.GetHashCode();
        hash ^= weekDays_.GetHashCode();
        if (slot_ != null) hash ^= Slot.GetHashCode();
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
      if (Type != global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto.Compound) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      weekDays_.WriteTo(output, _repeated_weekDays_codec);
      if (slot_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Slot);
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
        if (Type != global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto.Compound) {
            output.WriteRawTag(8);
            output.WriteEnum((int) Type);
        }
        weekDays_.WriteTo(ref output, _repeated_weekDays_codec);
        if (slot_ != null) {
            output.WriteRawTag(26);
            output.WriteMessage(Slot);
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
        if (Type != global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto.Compound) {
            size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
        }
        size += weekDays_.CalculateSize(_repeated_weekDays_codec);
        if (slot_ != null) {
            size += 1 + pb::CodedOutputStream.ComputeMessageSize(Slot);
        }
        if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
        }
        return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AvailabilityUnavailableWeekDaysDto other) {
        if (other == null) {
            return;
        }
        if (other.Type != global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto.Compound) {
            Type = other.Type;
        }
        weekDays_.Add(other.weekDays_);
        if (other.slot_ != null) {
            if (slot_ == null) {
                Slot = new global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TimeSlotDto();
            }
            Slot.MergeFrom(other.Slot);
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
            Type = (global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto) input.ReadEnum();
            break;
          }
          case 18:
          case 16: {
            weekDays_.AddEntriesFrom(input, _repeated_weekDays_codec);
            break;
          }
          case 26: {
            if (slot_ == null) {
              Slot = new global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TimeSlotDto();
            }
            input.ReadMessage(Slot);
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
                    Type = (global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.AvailabilityTypeDto) input.ReadEnum();
                    break;
                }
                case 18:
                case 16: {
                    weekDays_.AddEntriesFrom(ref input, _repeated_weekDays_codec);
                    break;
                }
                case 26: {
                    if (slot_ == null) {
                        Slot = new global::Ladesa.TimetableGenerator.Service.Infrastructure.Protos.TimeSlotDto();
                    }
                    input.ReadMessage(Slot);
                    break;
                }
            }
        }
    }
#endif

}