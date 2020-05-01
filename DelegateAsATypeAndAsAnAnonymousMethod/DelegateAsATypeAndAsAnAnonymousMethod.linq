<Query Kind="Program" />

void Main()
{
	var values = new[] { 1, 2, 3 };
	FinderPredicate pred = delegate (int valueToTest)
	{
		return valueToTest % 2 == 0;
	};
	values.Where(new Func<int, bool>(pred)).Dump();
}

delegate bool FinderPredicate(int valueToTest);