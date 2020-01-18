<Query Kind="Program" />

void Main()
{
	#nullable enable
	String? s = null;
	// Without an elvis operator there will be compile-time warning for "Nullable" string dereferencing.
	Console.WriteLine(s?.Length);
	
	// Compilers also performs code flow checking.
	if (s != null)
	{
		Console.WriteLine(s.Length);
	}
	
	// Supress the compiler warnings using "damn it" operator.
	if (!s!.IsNullOrEmpty())
	{
		Console.WriteLine(s!.Length);
	}
}

// Define other methods, classes and namespaces here
public static class StringExtensions
{
	public static bool IsNullOrEmpty(this string str)
	{
		return String.IsNullOrEmpty(str);
	}
}