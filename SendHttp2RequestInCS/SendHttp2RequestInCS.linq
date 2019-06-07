<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>D:\sources\Libs\packages\System.Net.Http.WinHttpHandler.4.4.0\lib\net461\System.Net.Http.WinHttpHandler.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

private HttpClient _client;

void Main()
{
	string _host = "https://localhost";
	string Path = "index.html";
	int Port = 8443;
	string _appName = "com.corrigo.corrigoenterprise";
	string TopicHeaderKey = "apns-topic";
	string Scheme = "https";
	// The following two from ApnsNotificationPayload
	string PayloadJson = "{\"aps\":{\"alert\":\"hi\",\"sound\":\"default\"}}";
	string DeviceToken = "BBEDB62FFE58B9B90A03E9FE456D283BED25926125D9002DDC8CC6C6FE7E9751";
	Initialize_client();
	MainImpl(_host, Path, Port, _appName, TopicHeaderKey, Scheme, PayloadJson, DeviceToken);
}

// Define other methods and classes here
private HttpClient Initialize_client()
{
//	X509Certificate _certificate = null;
//	Http2Handler handler = new Http2Handler
//	{
//		ClientCertificateOption = ClientCertificateOption.Manual
//	};
//	handler.ClientCertificates.Add(_certificate);
	Http2Handler handler = new Http2Handler();
	return _client ?? (_client = new HttpClient(handler));
}

private static Task<HttpResponseMessage> MainImpl(string _host,
string Path,
int Port,
string _appName,
string TopicHeaderKey,
string Scheme,
string PayloadJson,
string DeviceToken)
{
	var uri = new Uri(new UriBuilder(Scheme, _host, Port, Path + DeviceToken).ToString());
	using (HttpContent content = new StringContent(PayloadJson))
	using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, uri)
	{
		Content = content,
		Headers = { { TopicHeaderKey, _appName } }
	})
	{
		return _client.SendAsync(message);
	}
}

internal class Http2Handler: WinHttpHandler
{
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		request.Version = new Version(2, 0);
		return base.SendAsync(request, cancellationToken);
	}
}