using System.Text;

namespace FolderDiff.Models
{
	public class FileModel
	{
		public string Name { get; set; }

		/// <summary>
		/// The next four properties are only filled in for the files that fall into "different" category.
		/// </summary>
		public Encoding RemoteDetectedEncoding { get; set; }

		public Encoding LocalDetectedEncoding { get; set; }

		public string RemoteFileFirstLine { get; set; }

		public string LocalFileFirstLine { get; set; }
	}
}
