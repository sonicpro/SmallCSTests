<Query Kind="Program" />

void Main()
{
	//var transformer = x => new { Original = x, Normalized = x.ToLower() };
	
	//Func<string, { string Original, string Normalized }> transformer =>
	//x => new { Original = x, Normalized = x.ToLower() };
	
	// Can I use a lambda?
	var transformer = LambdaWrapper(x => new { Original = x, Normalized = x.ToLower() });
	Console.WriteLine(transformer("BOOM").Normalized);
}

// Yes, you can
private static Func<string, T> LambdaWrapper<T>(Func<string, T> lambda)
{
	return lambda;
}
