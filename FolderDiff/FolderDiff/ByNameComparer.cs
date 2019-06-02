using System.Collections.Generic;
using System.IO;

namespace FolderDiff
{
	public class ByNameComparer : IEqualityComparer<FileInfo>
	{
		public bool Equals(FileInfo fi1, FileInfo fi2)
		{
			return fi1.Name == fi2.Name;
		}

		public int GetHashCode(FileInfo fi)
		{
			return fi.Name.GetHashCode();
		}
	}
}
