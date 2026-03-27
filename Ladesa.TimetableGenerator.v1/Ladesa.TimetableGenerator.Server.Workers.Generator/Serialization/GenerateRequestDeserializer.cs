using System.Text;
using Ladesa.TimetableGenerator.Application.Generator.DTOs;
using Ladesa.TimetableGenerator.Application.Generator.Mappers;
using Ladesa.TimetableGenerator.Application.Ports;

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
