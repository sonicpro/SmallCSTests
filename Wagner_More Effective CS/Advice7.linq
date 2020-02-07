<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows</Namespace>
</Query>

void Main()
{
	var doubledCoord = Transform(new { X = 10, Y = 10 }, p => new Point { X = p.X * 2, Y = p.Y * 2 });
	doubledCoord.Dump();
}

// Define other methods and classes here
Point Transform<T>(T source, Func<T, Point> transformer)
{
	return transformer(source);
}