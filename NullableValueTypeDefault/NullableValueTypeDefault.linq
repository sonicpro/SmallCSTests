<Query Kind="Program" />

void Main()
{
    ColorStrings? a = default;
    if (typeof(ValueType).IsAssignableFrom(Nullable.GetUnderlyingType(typeof(ColorStrings?))))
    {
        Console.WriteLine("Bingo!");
        Console.WriteLine($"{(a == null ? "null" : a.ToString())}");
    }
}

// Define other methods and classes here
public enum ColorStrings
{
    Yellow,
}