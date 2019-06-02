using System.Text;
using System.Collections.Generic;

namespace FolderDiff.Models
{
	public class FileDiffModel : FileModel
	{
		/// <summary>
		/// For the following two properties -1 means that the corresponding file contains less lines than the file
		/// with the same name on the other side
		/// </summary>
		public int LocalFirstDifferenceIndex { get; set; } = -1;

		public int RemoteFirstDifferenceIndex { get; set; } = -1;

		public string LocalFirstDifferenceText { get; set; } = string.Empty;

		public string RemoteFirstDifferenceText { get; set; } = string.Empty;
	}
}
