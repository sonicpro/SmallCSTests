using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SendHttp2RequestInCS
{
	class Program
	{
		private static HttpClient _client;

		public static void Main(string[] args)
		{
			string _host = "localhost";
			string Path = "index.html";
			int Port = 8443;
			string Scheme = "https";
			Initialize_client();
			HttpResponseMessage response = MainImpl(_host, Path, Port, Scheme).Result;
			string body = response.Content.ReadAsStringAsync().Result;
			Console.WriteLine(body);
			Console.ReadLine();
		}

		// Define other methods and classes here
		private static void Initialize_client()
		{
			if (_client != null)
				return;
			Http2Handler handler = new Http2Handler();
			_client = new HttpClient(handler);
		}

		private static Task<HttpResponseMessage> MainImpl(string host,
			string path,
			int port,
			string scheme)
		{
			var uri = new Uri(new UriBuilder(scheme, host, port, path).ToString());
			return _client.GetAsync(uri);
		}
	}
}
