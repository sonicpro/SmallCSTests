<Query Kind="Program" />

void Main()
{
	Child c = new Child();
	c.Print();
}

// Define other methods and classes here
abstract class Base {
	protected abstract void Print();
}
	
class Child : Base
{
	protected override void Print() { Console.WriteLine("Child"); }
}
