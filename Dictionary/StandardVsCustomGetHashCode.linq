<Query Kind="Program" />

void Main()
{
	var dict = new Dictionary<TestRefKey, string>();
	var key1 = new TestRefKey(5);
	dict.Add(key1, "hello");
	var key2 = new TestRefKey(5);
	// The next line throws "KeyNotFoundException".
	// Console.WriteLine(dict[key2]);
	
	var dictForAGoodClass = new Dictionary<CorrectTestRefKey, string>();
	var key1ValueTypeSemantics = new CorrectTestRefKey(5);
	dictForAGoodClass.Add(key1ValueTypeSemantics, "hello");
	var key2ValueTypeSemantics = new CorrectTestRefKey(5);
	Console.WriteLine(dictForAGoodClass[key2ValueTypeSemantics]);
}

// Define other methods and classes here
public class TestRefKey
{
	public readonly int Key;
	
	public TestRefKey(int key)
	{
		Key = key;
	}
}

public class CorrectTestRefKey
{
	// If the HashCode calculation is based on the field, immutability is crucial.
	public readonly int Key;

	public CorrectTestRefKey(int key)
	{
		Key = key;
	}

	public override bool Equals(object obj)
	{
		if (base.Equals(obj))
		{
			return true;
		}

		if (obj is CorrectTestRefKey otherKey)
		{
			return this.Key == otherKey.Key;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return this.Key.GetHashCode();
	}
}