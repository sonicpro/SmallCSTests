<Query Kind="Program">
  <Namespace>System.Collections.Concurrent</Namespace>
</Query>

void Main()
{
    Console.WriteLine(ThrottledMessageRegistry.Instance.GetQuruedMessageCount(typeof(System.Drawing.Color)));
}

// Define other methods and classes here
public class ThrottledMessageRegistry
{
    ConcurrentDictionary<Type, int> _throttledTypes = new ConcurrentDictionary<Type, int>();
    private static object @lock = new object();
    private static ThrottledMessageRegistry _instance;
    private ThrottledMessageRegistry()
    {
        Console.WriteLine($"Constructor run. Instance field is {(_throttledTypes != null ? "already" : "not")} initialized.");
    }
    
    public static ThrottledMessageRegistry Instance
    {
        get
        {
            lock (@lock)
            {
                if (_instance == null)
                {
                    _instance = new ThrottledMessageRegistry();
                }
                
                return _instance;
            }
        }
    }
    
    public int GetQuruedMessageCount(Type messageType)
    {
        if (!_throttledTypes.TryGetValue(typeof(System.Drawing.Color), out var count))
        {
            return 0;
        }
        
        return count;
    }
}