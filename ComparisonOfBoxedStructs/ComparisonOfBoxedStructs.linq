<Query Kind="Program" />

void Main()
{
    object a = 1;
    object b = 1;
    Console.WriteLine(a is int);
    Console.WriteLine(a.Equals(b));

    // Also works with boxed Nullable<T> structs.
    object nullableA = new Nullable<int>(1);
    object nullableB = new Nullable<int>(1);
    Console.WriteLine(nullableA == nullableB);

    Console.WriteLine(nullableA.Equals(nullableB));

    // I did not find the confirmation that calling Equals on the boxed instance called the wrapped tipe override.
    // Trying to imagine how does it implemented.
    object intLikeA = new IntegerLike(1);
    object intLikeB = new IntegerLike(1);
    Console.WriteLine(intLikeA == intLikeB);
    Console.WriteLine(intLikeA.Equals(intLikeB));
}

// Define other methods and classes here
public struct IntegerLike
{
    int _value;
    public IntegerLike(int value)
    {
        _value = value;
    }
    
    public int GetValue()
    {
        return _value;
    }
    
    public override bool Equals(object b)
    {
        if (b is int)
        {
            return _value == (int) b;
        }
        
        if (b is IntegerLike)
        {
            Console.WriteLine("We are in the right comparison!");
            return _value == ((IntegerLike)b).GetValue();
        }
        
        return base.Equals(b);
    }


    public override int GetHashCode()
    {
        return _value;
    }
}