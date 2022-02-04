<Query Kind="Program">
  <Namespace>System.Text.Json</Namespace>
</Query>

void Main()
{
		var steps = this.Digits().GetEnumerator();
		var hasSteps = steps.MoveNext();
        while (true)
		{
			var current = steps.Current;
			if (steps.MoveNext())
				Console.WriteLine(current);
			else
			{
				Console.WriteLine($"last d:{steps.Current}");
				return;
			}
		}
}

// You can define other methods, fields, classes and namespaces here
private IEnumerable<int> Digits()
{
	yield return 1;
	yield return 2;
}