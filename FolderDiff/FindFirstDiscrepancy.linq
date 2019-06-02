<Query Kind="Program">
  <Namespace>System.IO</Namespace>
</Query>

void Main()
{
	var local = Directory.GetFiles(@"D:\SourcesCS\SmallCSTests\FolderDiff\Ucs2");
	var remote = Directory.GetFiles(@"D:\SourcesCS\SmallCSTests\FolderDiff\Utf8");

	var localFilePath = local.First(n => Path.GetFileName(n) == "!output.txt");
	var remoteFilePath = remote.First(n => Path.GetFileName(n) == "!output.txt");
	Console.WriteLine(FirstDiscrepancyFinder(localFilePath, remoteFilePath));
	
}

private static int FirstDiscrepancyFinder(string firstFilePath, string secondFilePath)
{
	var firstFileContents = File.ReadAllLines(firstFilePath);
	var secondFileContents = File.ReadAllLines(secondFilePath);
	
	var isFirstFileShorter = firstFileContents.Length < secondFileContents.Length;
	
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
