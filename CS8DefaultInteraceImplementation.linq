<Query Kind="Program">
  <RuntimeVersion>3.1</RuntimeVersion>
</Query>

class Program
{
    static void Main(string[] args)
    {
        ILogger<int, string> test = new ConsoleLogger();
        test.Send("test");
    }
}
public interface ILogger<T, U> : IBase<T, U> where T: new()
{
    void Send(string text) => Console.WriteLine(text);
}

public interface IBase<T, U> where T: new()
{
	public T GetData(U param);
}

public class ConsoleLogger : ILogger<int, string>
{
	public int GetData(string param)
	{
		return 1;
	}
}