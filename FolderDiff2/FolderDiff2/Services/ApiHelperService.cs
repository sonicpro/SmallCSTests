using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace FolderDiff2.Services
{
	public class ApiHelperService
	{
		private readonly string host;

		private readonly string httpScheme;

		private readonly int port;

		private readonly string authorizationScheme;

		private readonly string path;

		private readonly AuthorizationHelperService authService;

		public ApiHelperService(IOptions<ApiOptions> options, AuthorizationHelperService authHelperService)
		{
			host = options.Value.Host;
			httpScheme = options.Value.HttpScheme;
			port = options.Value.Port;
			authorizationScheme = options.Value.AuthorizationScheme;
			authService = authHelperService;
			path = options.Value.Path;
		}

		public async Task<string> GetFolderDataFromApi()
		{
			AccessToken token = authService.AcquireTokenAsync().Result;

			var apiUrl = new UriBuilder(httpScheme, host, port, path).ToString();

			using (var client = new HttpClient())
			using (var request = new HttpRequestMessage(HttpMethod.Post, apiUrl))
			{
				request.Headers.Authorization = new AuthenticationHeaderValue(authorizationScheme, token.access_token);
				var response = await client.SendAsync(request);
				return await response.Content.ReadAsStringAsync();
			}
		}
	}
}
