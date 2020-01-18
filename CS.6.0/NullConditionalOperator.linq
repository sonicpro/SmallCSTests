<Query Kind="Program" />

void Main()
{
	cD inst = new cD();
				
	// OK
	Console.WriteLine(inst.getB);
	
	// Do not put the null propagation operator too early.
	// If it is put next to non-null variable ("inst") below, it won't work.
	
	// Console.WriteLine(inst?.getB.getA);
	
	
	// Nor put it too late ("getB" returns null and no propagation is specified for it).
	
	// Console.WriteLine(inst.getB.getA?.aText);
	
	// This is OK.
	Console.WriteLine(inst.getB?.getA.aText);
}

// Define other methods and classes here
class cA
{
	public string aText { get; }
}

class cD
{
	public cB getB { get; }
}	
	
class cB
{
	public cA getA { get; }
}