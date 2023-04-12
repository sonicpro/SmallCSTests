<Query Kind="Program" />

void Main()
{
    MutableStructHolder holder = new MutableStructHolder();
    // Affects the value of holder.Field
    holder.Field.SetValue(10);
    // Retrieves holder.Property (it has a value type!) as a copy and changes the copy
    holder.Property.SetValue(10);

    Console.WriteLine(holder.Field.Value);
    Console.WriteLine(holder.Property.Value);
    
    holder.AField = 12;
    holder.AnotherValueTypeProperty = 12;
    Console.WriteLine(holder.AField);
    Console.WriteLine(holder.AnotherValueTypeProperty);
}

// Define other methods and classes here
struct MutableStruct
{
    public int Value { get; set; }

    public void SetValue(int newValue)
    {
        Value = newValue;
    }
}

class MutableStructHolder
{
    public MutableStruct Field;
    public MutableStruct Property { get; set; }
    public int AField;
    public int AnotherValueTypeProperty { get; set; }
}