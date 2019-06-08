using System.Collections.Generic;

namespace FolderDiff2.Models
{
	public class FolderDiffModel
	{
		public IList<string> LocalOnly { get; set; }

		public IList<string> RemoteOnly { get; set; }

		public IList<string> Same { get; set; }

		public IList<string> Different { get; set; }
	}
}
