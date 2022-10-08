<Query Kind="Program" />

void Main()
{
	var filesWithDetections = new List<string>();
	FileProcessor(@"C:\repos\CRM.Client.ecivreSdleiF.Mobile\", filesWithDetections, BadSqlChecker);
	filesWithDetections.Dump();
}


private static readonly Regex sql = new Regex("\bDELETE|WHERE|AND|FROM|SELECT|JOIN|OR\b");
private static readonly Regex sqlConcat = new Regex(@"""(\s*\+\s*)[\w\.]+\1?""");

// Define other methods and classes here
private void FileProcessor(string dirPath, List<string> filesWithDetection, Action<FileInfo, List<string>> processor)
{
	var dir = new DirectoryInfo(dirPath);
	dir.GetFiles().Where(f => f.Extension == ".cs")
		.ToList().ForEach(f => processor(f, filesWithDetection));
	var children = dir.GetDirectories();
	if (children.Any())
	{
		string[] paths = children.Select(d => d.FullName)
			.ToArray();
		foreach (string p in paths)
		{
			FileProcessor(p, filesWithDetection, processor);
		}
	}
}

private void BadSqlChecker(FileInfo f, List<string> filesWithDetections)
{
	var lines = File.ReadLines(f.FullName);

	var calls = lines.Where(l => sql.IsMatch(l) && sqlConcat.IsMatch(l));
	if (calls.Any())
	{
		filesWithDetections.Add(f.FullName);
	}
}