<Query Kind="Program" />

void Main()
{
	Dictionary<string, SearchModeState> result = new (){ { "test1", SearchModeState.First }, { "test2", SearchModeState.Second }};
}

// You can define other methods, fields, classes and namespaces here
public enum SearchModeState
{
	First, Second
	}