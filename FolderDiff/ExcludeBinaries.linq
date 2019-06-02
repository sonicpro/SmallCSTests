<Query Kind="Program" />

void Main()
{
	var comparer = new TextFileComparer(false);
	
	var localDirectoryContents = new DirectoryInfo(@"D:\SourcesCS\SmallCSTests\FolderDiff\Ucs2");
	var remoteDirectoryContents = new DirectoryInfo(@"D:\SourcesCS\SmallCSTests\FolderDiff\Utf8");

	var localIgnored = localDirectoryContents.GetFiles().Where(SupposedlyBinaryFile);
	var localTextFiles = localDirectoryContents.GetFiles().Where(fi => !localIgnored.Contains(fi, comparer));
	foreach (var f in localTextFiles)
	{
		Console.WriteLine(f.Name);
	}
	localIgnored.Dump();
}

// Define other methods and classes here

private static bool SupposedlyBinaryFile(FileInfo fi)
{
	var buffer = new char[1000];
	using (var fs = new FileStream(fi.FullName, FileMode.Open))
	using (var sr = new StreamReader(fs, true))
	{
		sr.ReadBlock(buffer, 0, 1000);
		return ContainsNulChars(buffer);
	}
}

private static bool ContainsNulChars(char[] chars)
{
	return chars.Any(c => c == '0');
}

public class FileModel
{
	public string Name { get; set; }
}

public class TextFileComparer : IEqualityComparer<FileInfo>
{
	private bool ignoreEncoding;

	public TextFileComparer(bool ignoreEncoding)
	{
		this.ignoreEncoding = ignoreEncoding;
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