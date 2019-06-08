using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FolderDiff2.Services
{
	public class AuthorizationHelperService
	{
		private readonly string grantType;

		private readonly string tokenUrl;

		private readonly string clientId;

		private readonly string clientSecret;

		public AuthorizationHelperService(IOptions<AuthServerOptions> options)
		{
			tokenUrl = options.Value.TokenUrl;
			clientId = options.Value.ClientId;
			clientSecret = options.Value.ClientSecret;
			grantType = options.Value.GrantType;
		}

		public async Task<AccessToken> AcquireTokenAsync()
		{
			using (var client = new HttpClient())
			{
				// Headers.
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue("application/json"));

				var content = GetBody(grantType, clientId, clientSecret);
				var response = await client.PostAsync(tokenUrl, content);
				var messageString = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<AccessToken>(messageString);
			}
		}

		#region Helper Methods

		private static FormUrlEncodedContent GetBody(string grantType, string clientId, string clientSecret)
		{
			var formData = new List<KeyValuePair<string, string>>();
			formData.Add(new KeyValuePair<string, string>("client_id", clientId));
			formData.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
			formData.Add(new KeyValuePair<string, string>("client_id", clientId));

			return new FormUrlEncodedContent(formData);
		}

		#endregion
	}
}
