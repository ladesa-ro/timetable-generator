using System.Text;
using Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;
using Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Serialization;

public class GenerateResponseSerializer : IMessageSerializer<ServiceGenerateResponseDto>
{
    public byte[] Serialize(ServiceGenerateResponseDto message)
    {
        var messagesDto = ServiceGenerateResponseMapper.ToMessagesDto(message);
        var json = Msg.Serialize.ToJson(messagesDto);
        return Encoding.UTF8.GetBytes(json);
    }
}
