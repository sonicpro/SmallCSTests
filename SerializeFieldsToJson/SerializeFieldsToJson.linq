<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <NuGetReference Version="12.0.3">Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
</Query>

void Main()
{
    var a = new Client { ClientGuid = Guid.NewGuid(), Name = "Test client" };
    File.WriteAllText(@"C:\temp\serializableClient.json", JsonConvert.SerializeObject(a, typeof(Client), serializerSettings));
}

// Define other methods and classes here
private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
{
    ContractResolver = new DefaultContractResolver { IgnoreSerializableAttribute = false }
};

[Serializable]
public class Client : PushMessage
{
    /// <summary>
    /// The name of the client.
    /// </summary>
    [DataMember]
    public string Name = null;

    /// <summary>
    /// A GUID identifying the client uniquely.
    /// </summary>
    [DataMember]
    public Guid ClientGuid = Guid.Empty;

    public override string ToString()
    {
        if (Name != null)
        {
            return Name + " w. guid " + ClientGuid.ToString();
        }

        return "uninitialized Client";
    }
}


public class PushMessage : MessageBase
{
    public PushMessage()
    {
    }
}

[KnownType(typeof(PushMessage))]
[KnownType(typeof(Client))]
[DataContract]
[Serializable]
public abstract class MessageBase
{
}