using System.Collections.Generic;

namespace FolderDiff.Models
{
	public class FolderDiffModel
	{
		public IList<FileModel> LocalOnly { get; set; }

		public IList<FileModel> RemoteOnly { get; set; }

		public IList<FileModel> Different { get; set; }

		public IList<FileModel> LocalIgnored { get; set; }

		public IList<FileModel> RemoteIgnored { get; set; }
	}
}
