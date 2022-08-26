<Query Kind="Program" />

void Main()
{
    var vi = new VehicleInfo
    {
      Type = new MetadataInfo { Value = "abc" }  
    };
    
    var pi = vi.GetType().GetProperties().FirstOrDefault(v => v.Name == "Type");
    var viValue = pi.GetValue(vi);
    var mipi = viValue.GetType().GetProperties().FirstOrDefault(v => v.Name == "Value");
    Console.WriteLine(mipi.GetValue(viValue));
}

// Define other methods and classes here
public class VehicleInfo
{
    public MetadataInfo Type { set; get; }
}

public class MetadataInfo
{
    public string Value { set; get; }
}