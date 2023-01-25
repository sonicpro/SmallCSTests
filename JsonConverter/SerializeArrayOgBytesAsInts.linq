<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

// Taken from https://stackoverflow.com/questions/15226921/how-to-serialize-byte-as-simple-json-array-and-not-as-base64-in-json-net/15228384#15228384        class ClientMessage
private const string DumpLocation = @"C:\temp\SerializedMessage.txt";

internal void Main()
{
    var message = new ClientMessage { _bytessent = 0, _content = new[] { (byte)0x41, (byte)0x40 }, _type = ContentTypes.SoapObject };
    using (var messageDump = File.OpenWrite(DumpLocation))
    {
        Serialize(message, messageDump);
    }
    
    ClientMessage originalMessage;
    using (var reader = File.OpenRead(DumpLocation))
    {
        originalMessage = DeserializeFromStream<ClientMessage>(reader);
    }
    
    for (int i = 0; i < originalMessage._content.Length; i++)
    {
        Console.WriteLine("0x{0:X}", originalMessage._content[i]);
    }
}

private static T DeserializeFromStream<T>(Stream stream) where T: class
{
    var serializer = new JsonSerializer();

    using (var sr = new StreamReader(stream))
    using (var jsonTextReader = new JsonTextReader(sr))
    {
        return serializer.Deserialize(jsonTextReader, typeof(T)) as T;
    }
}

private static void Serialize(object value, Stream s)
{
    using (StreamWriter writer = new StreamWriter(s))
    using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
    {
        JsonSerializer ser = new JsonSerializer();
        ser.Serialize(jsonWriter, value);
        jsonWriter.Flush();
    }
}

public enum ContentTypes : byte
{
    /// <summary>
    /// If content is binary.
    /// </summary>
    Binary,
    /// <summary>
    /// If content is a SOAP.
    /// </summary>
    SoapObject,
    /// <summary>
    /// If content is a XML.
    /// </summary>
    XML,
    /// <summary>
    /// BufferSize
    /// </summary>
    BufferSize// used in EventCoordinator.VideoWalls.ServerVideoWalls.GotWelcome to send message to video walls
}

public class ClientMessage
{
    public ContentTypes _type;
    [JsonConverter(typeof(ByteArrayConverter))]
    public byte[] _content;
    public int _bytessent;
}

public class ByteArrayConverter : JsonConverter
{
    public override void WriteJson(
        JsonWriter writer,
        object value,
        JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        byte[] data = (byte[])value;

        // Compose an array.
        writer.WriteStartArray();

        for (var i = 0; i < data.Length; i++)
        {
            writer.WriteValue(data[i]);
        }

        writer.WriteEndArray();
    }

    public override object ReadJson(
        JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartArray)
        {
            var byteList = new List<byte>();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Integer:
                        byteList.Add(Convert.ToByte(reader.Value));
                        break;
                    case JsonToken.EndArray:
                        return byteList.ToArray();
                    case JsonToken.Comment:
                        // skip
                        break;
                    default:
                        throw new Exception(
                        string.Format(
                            "Unexpected token when reading bytes: {0}",
                            reader.TokenType));
                }
            }

            throw new Exception("Unexpected end when reading bytes.");
        }
        else
        {
            throw new Exception(
                string.Format(
                    "Unexpected token parsing binary. "
                    + "Expected StartArray, got {0}.",
                    reader.TokenType));
        }
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(byte[]);
    }
}