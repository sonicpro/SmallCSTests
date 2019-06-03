using System.Text;
using Newtonsoft.Json;

namespace FolderDiff.Models
{
	public class FileDiffModel : FileModel
	{
		public int DifferentLineIndex { get; set; }

		public string LocalFirstDifferentLineText { get; set; }

		public string RemoteFirstDifferentLineText { get; set; }

		[JsonConverter(typeof(JsonEncodingConverter))]
		public Encoding RemoteDetectedEncoding { get; set; }

		[JsonConverter(typeof(JsonEncodingConverter))]
		public Encoding LocalDetectedEncoding { get; set; }
	}
}
