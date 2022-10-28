<Query Kind="Program" />

void Main()
{
    var old = new ActiveSubscription { b = new Test("aba") };
    var newClients = new ActiveSubscription { b = new Test()};
    var strings = old.b._c;
    CopyC(newClients, strings);
    foreach(var str in newClients.b._c)
        Console.WriteLine(str);
}

// Define other methods and classes here
class ActiveSubscription { public Test b;}
class Test
{
    public Test()
    {
        _c = new List<string>();
    }
    public Test(string c)
    {
        _c = new List<string>();
        _c.Add(c);
    }
    public List<string> _c;
}

private static void CopyC(ActiveSubscription s, List<string> oldStrings)
{
    s.b._c.AddRange(oldStrings);
}