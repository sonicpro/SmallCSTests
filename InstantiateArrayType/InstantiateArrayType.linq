<Query Kind="Program">
  <Reference>&lt;NuGet&gt;\murmurhash\1.0.3\lib\netstandard2.0\MurmurHash.dll</Reference>
  <Namespace>Murmur</Namespace>
</Query>

void Main()
{
	var array = new TransientFile[] {};
	Type elementType = array.GetType().GetElementType();
	var arrayCreatedThroughReflection = Array.CreateInstance(elementType, 0);
	Console.WriteLine(array.GetType() == arrayCreatedThroughReflection.GetType());
}

// You can define other methods, fields, classes and namespaces here
public class TransientFile
{
    private static readonly Encoding Encoding = new UTF8Encoding();
    private static readonly Murmur32 Algorithm = MurmurHash.Create32(5);

    public string Name { get; set; }

    public string InternalName { get; set; }

    public int? Order { get; set; }

    public int? Page { get; set; }

    public Dictionary<string, string> Headers { get; set; }

    public DocumentType? Type { get; set; }

    public string Category { get; set; }

    public string Url { get; set; }

    public string Path { get; set; }

    public bool? UseAuth { get; set; }

    public bool? FetchFromLocalS3 { get; set; }

    public string ProductName { get; set; }

    public string DocType { get; set; }

    public uint Hash
    {
        get
        {
            var bytes = Encoding.GetBytes(this.Url);
            var hash = Algorithm.ComputeHash(bytes);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(hash);
            }
            return BitConverter.ToUInt32(hash);
        }
    }

    public string HashedFileName
    {
        get
        {
            bool fetchFromLocalS3 = this.FetchFromLocalS3.GetValueOrDefault();
            if (!fetchFromLocalS3)
            {
                return $"{Hash}.pdf";
            }
            return Url.Split('/')[^1];
        }
    }
}

public enum DocumentType
{
    Approval,
    Custom,
    Product
}