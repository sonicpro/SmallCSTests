using System.Buffers;
using System.Text.Json;

namespace Parsing
{
    public static class JsonElementExtensions
    {
        public static T ToObject<T>(this JsonElement element, JsonSerializerOptions options = null)
        {
            if (options == null)
            {
                options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            }

            var bufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(bufferWriter))
            {
                element.WriteTo(writer);
            }
            return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, options);
        }
    }
}
