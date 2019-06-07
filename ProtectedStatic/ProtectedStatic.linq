<Query Kind="Program" />

void Main()
{
	Console.WriteLine(Child.GetPassword());
	Console.WriteLine(Malicious.StealPassword());
	if (Child.GetPassword() == "I am completely different!")
	{
		Console.WriteLine("Boom!");
	}
}

// Define other methods and classes here
public class Base

{

	protected static String _password;

}

public class Child : Base

{

	static Child()

	{

		_password = "pwd";

	}

	public static string GetPassword()

	{ return _password; }

}

public class Malicious : Base

{
	static Malicious()
	{
		_password = "I am completely different!";
	}
	public static string StealPassword()

	{ return _password;}

}
