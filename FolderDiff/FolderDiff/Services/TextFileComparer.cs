using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderDiff
{
	public class TextFileComparer : IEqualityComparer<FileInfo>
	{
		private bool ignoreEncoding;

		public TextFileComparer(IOptions<FolderDiffOptions> settings)
		{
			this.ignoreEncoding = settings.Value.IgnoreEncoding;
		}

		public bool Equals(FileInfo f1, FileInfo f2)
		{
			if (!ignoreEncoding)
			{
				var buffer = new char[1000];
				using (var fs1 = new FileStream(f1.FullName, FileMode.Open, FileAccess.Read))
				using (var sr1 = new StreamReader(fs1, true))
				using (var fs2 = new FileStream(f2.FullName, FileMode.Open, FileAccess.Read))
				using (var sr2 = new StreamReader(fs2, true))
				{
					sr1.ReadBlock(buffer, 0, 1000);
					sr2.ReadBlock(buffer, 0, 1000);
					if (sr1.CurrentEncoding.GetType() != sr2.CurrentEncoding.GetType())
					{
						return false;
					}
				}
			}

			return File.ReadLines(f1.FullName).SequenceEqual(File.ReadLines(f2.FullName));
		}

		public int GetHashCode(FileInfo file)
		{
			if (ignoreEncoding)
			{
				return file.Name.GetHashCode();
			}
			else
			{
				string s = $"{file.Name}{file.Length}";
				return s.GetHashCode();
			}
		}
	}
}
