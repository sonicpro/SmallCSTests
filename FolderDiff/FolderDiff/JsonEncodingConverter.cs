using System;
using System.Text;
using Newtonsoft.Json;

namespace FolderDiff
{
	public class JsonEncodingConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(Encoding).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			// We are not going to deserialize FileDiffModel json-serialized instances.
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, (value as Encoding).WebName);
		}
	}
}
