using System.Text.Json;
using Framework.Utils;

namespace Framework.Infrastructure.Serialization
{
    public sealed class JsonSerializationProvider : SerializationProvider
    {
        public override T Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        public override string Serialize<T>(T element)
        {
            return JsonSerializer.Serialize(element);
        }
    }
}
