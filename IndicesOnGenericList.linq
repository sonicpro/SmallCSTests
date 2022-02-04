<Query Kind="Statements" />

var lines = new List<int> { 1, 2 };
int length;
if ((length = lines.Count) > 0)
{
	foreach (var line in lines.GetRange(0, length - 1))
	{
		Console.WriteLine(line);
	}
}
if (length > 0)
{
	Console.WriteLine(lines[^1]);
}
	