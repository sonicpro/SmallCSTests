<Query Kind="Program">
  <Namespace>System.IO</Namespace>
</Query>

void Main()
{
	var local = Directory.GetFiles(@"D:\SourcesCS\SmallCSTests\FolderDiff\Ucs2");
	var buffer = new char[1000];
	
	// !output.txt is Ucs2-encoded.
	string unicode = local.First(n => Path.GetFileName(n) == "!output.txt");
	using (var fsLocal = new FileStream(unicode, FileMode.Open))
	using (var srLocal = new StreamReader(fsLocal, true))
	{
		srLocal.ReadBlock(buffer, 0, 1000);
		Console.WriteLine($"{unicode} is {(ContainsNulChars(buffer) ? "binary" : "text")} file");
		Console.WriteLine(srLocal.CurrentEncoding.GetType() == typeof(System.Text.UnicodeEncoding));
	}
	
	var remote = Directory.GetFiles(@"D:\SourcesCS\SmallCSTests\FolderDiff\Utf8");
	string utf8 = remote.First(n => Path.GetFileName(n) == "!output.txt");
	using (var fsRemote = new FileStream(utf8, FileMode.Open))
	using (var srRemote = new StreamReader(fsRemote, true))
	{
		srRemote.ReadBlock(buffer, 0, 1000);
		Console.WriteLine($"{utf8} is {(ContainsNulChars(buffer) ? "binary" : "text")} file");
		Console.WriteLine(srRemote.CurrentEncoding.GetType() == typeof(System.Text.UTF8Encoding));
	}

	string binary = local.First(n => Path.GetFileName(n) == "LINQAndEqualityComparer.zip");
	using (var fsBinaryLocal = new FileStream(binary, FileMode.Open))
	using (var srBinaryLocal = new StreamReader(fsBinaryLocal, true))
	{
		srBinaryLocal.ReadBlock(buffer, 0, 1000);
		Console.WriteLine($"{binary} is {(ContainsNulChars(buffer) ? "binary" : "text")} file");
		Console.WriteLine(srBinaryLocal.CurrentEncoding.GetType().Name);
	}
	
	// Two more checks for Ucs2 text files.
	string article = local.First(n => Path.GetFileName(n) == "abstract.txt");
	string shruggy = local.First(n => Path.GetFileName(n) == "shruggy.txt");
	using (var fsTextLocal = new FileStream(article, FileMode.Open))
	using (var srTextLocal = new StreamReader(fsTextLocal, true))
	{
		srTextLocal.ReadBlock(buffer, 0, 1000);
		Console.WriteLine($"{article} is {(ContainsNulChars(buffer) ? "binary" : "text")} file");
	}

	using (var fsTextLocal2 = new FileStream(shruggy, FileMode.Open))
	using (var srTextLocal2 = new StreamReader(fsTextLocal2, true))
	{
		srTextLocal2.ReadBlock(buffer, 0, 1000);
		Console.WriteLine($"{shruggy} is {(ContainsNulChars(buffer) ? "binary" : "text")} file");
	}

	// And a final check for the pdf:
	string pdf = remote.First(n => Path.GetFileName(n) == "O(NP).pdf");
	using (var pdfRemote = new FileStream(pdf, FileMode.Open))
	using (var srBinaryRemote = new StreamReader(pdfRemote, true))
	{
		srBinaryRemote.ReadBlock(buffer, 0, 1000);
		Console.WriteLine($"{pdf} is {(ContainsNulChars(buffer) ? "binary" : "text")} file");
	}
}

// Define other methods and classes here
static bool ContainsNulChars(char[] chars)
{
	return chars.Any(c => c == '0');
}
