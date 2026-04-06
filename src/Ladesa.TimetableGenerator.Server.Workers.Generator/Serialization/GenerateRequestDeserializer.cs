using System.Text;
using Ladesa.TimetableGenerator.Application.Ports;
using Ladesa.TimetableGenerator.Server.Workers.Generator.DTOs;
using Ladesa.TimetableGenerator.Server.Workers.Generator.Mappers;

namespace Ladesa.TimetableGenerator.Server.Workers.Generator.Serialization;

public class GenerateRequestDeserializer : IMessageDeserializer<ServiceGenerateRequestDto>
{
    public ServiceGenerateRequestDto Deserialize(byte[] bytes)
    {
        var json = Encoding.UTF8.GetString(bytes);
        var messagesDto = Msg.GenerateRequest.FromJson(json);
        return ServiceGenerateRequestMapper.ToServiceDto(messagesDto);
    }
}
