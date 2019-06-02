<Query Kind="Program">
  <Namespace>System.IO</Namespace>
</Query>

void Main()
{
	var local = Directory.GetFiles(@"D:\SourcesCS\SmallCSTests\FolderDiff\Ucs2");
	var remote = Directory.GetFiles(@"D:\SourcesCS\SmallCSTests\FolderDiff\Utf8");

	var localFilePath = local.First(n => Path.GetFileName(n) == "a.txt");
	var remoteFilePath = remote.First(n => Path.GetFileName(n) == "a.txt");
	Console.WriteLine(FirstDiscrepancyFinder(localFilePath, remoteFilePath));
	
}

private int FirstDiscrepancyFinder(string firstFilePath, string secondFilePath)
{
	var isFirstFileShorter = File.ReadAllLines(firstFilePath).Length <
		File.ReadAllLines(secondFilePath).Length;
	if (isFirstFileShorter)
		Console.WriteLine("Bingo!");
	IEnumerable<string> longerContents = File.ReadLines(isFirstFileShorter ? secondFilePath : firstFilePath);
	IEnumerable<string> shorterContents = File.ReadLines(isFirstFileShorter ? firstFilePath : secondFilePath);
	int differentLineIndex = 0;
	if (shorterContents.Where((line, i) =>
		{
			differentLineIndex = i;
			return longerContents.Skip(i).First() != line;
		}).Any())
	{
		return differentLineIndex;
	}
	return ++differentLineIndex;
}
