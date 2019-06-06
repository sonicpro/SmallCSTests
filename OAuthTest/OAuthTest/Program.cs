using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OAuthTest
{
	class Program
	{
		private const string authGrant = "grant_type";
		private const string grantType = "client_credentials";
		private const string clientId = "MartinsburgMonster";
		private const string clientSecret = "bb3ab7975429748834c28686d3886840";

		static void Main(string[] args)
		{
			Console.WriteLine("Coop API (http://http://coop.apps.symfonycasts.com).");
			AccessToken token = AcquireTokenAsync().Result;
			Console.WriteLine($"AccessToken: {token.access_token}");
		}


		#region Helper Methods

		private static async Task<AccessToken> AcquireTokenAsync()
		{
			using (var client = new HttpClient())
			{
				// Define headers
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue("application/json"));

				var content = GetBody(authGrant, grantType, clientId, clientSecret);
				var response = await client.PostAsync("http://coop.apps.knpuniversity.com/token", content);
				var messageString = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<AccessToken>(messageString);
			}
		}

		private static FormUrlEncodedContent GetBody(string authGrant,
			string grantType,
			string clientId,
			string clientSecret)
		{
			var formData = new List<KeyValuePair<string, string>>();
			formData.Add(new KeyValuePair<string, string>("client_id", clientId));
			formData.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
			formData.Add(new KeyValuePair<string, string>(authGrant, grantType));

			return new FormUrlEncodedContent(formData);
		}

		#endregion
	}
}
