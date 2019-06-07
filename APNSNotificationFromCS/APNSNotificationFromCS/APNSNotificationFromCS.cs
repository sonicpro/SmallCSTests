using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace APNSNotificationFromCS
{
	class Program
	{
		private static HttpClient _client;
		private const string TopicHeaderKey = "apns-topic";
		private const string AppName = "com.corrigo.corrigoenterprise";
		private static X509Certificate _certificate;

		public static void Main(string[] args)
		{
			string _host = "api.push.apple.com";
			string Path = @"3/device/";
			int Port = 443;
			string Scheme = "https";
			Initialize_client();
			string response = MainImpl(_host, Path, Port, Scheme);
			Console.WriteLine(response);
			Console.ReadLine();
		}

		#region Helper Functionality

		private static X509Certificate2Collection FindPushCertificate(X509Certificate2Collection baseList)
		{
			return baseList
				.Find(X509FindType.FindBySubjectName, "apple", true)
				.Find(X509FindType.FindBySubjectName, "push service", true)
				.Find(X509FindType.FindBySubjectName, AppName, true);
		}

		// Obtains X509 client certificates from local certificate storage
		private static void LoadCertificates()
		{
			if (_certificate != null) return;

			X509CertificateCollection certificates = null;
			// Obtain SSL certificate from X.509 store of current user
			var certStore = new X509Store(StoreLocation.LocalMachine);
			certStore.Open(OpenFlags.ReadOnly);
			try
			{

				var baseCertificatesList = certStore.Certificates;
				var nocCertificate = LoadCertificateFromNoc();

				certificates = FindPushCertificate(baseCertificatesList);

				// add nocCertificate to the certificates in the local computer storage 
				// to make it valid through chain of certificates
				// also test if we already have proper certificate in the computer's local storage
				if ((certificates.Count == 0) && (nocCertificate != null))
				{
					Console.WriteLine("-----------------------");
					Console.WriteLine("nocCertificate was used");
					baseCertificatesList.Add(nocCertificate);
					certificates = FindPushCertificate(baseCertificatesList);
					Console.WriteLine("-----------------------");
				}
				else
				{
					Console.WriteLine("-----------------------");
					Console.WriteLine("certStore certificate was used");
					Console.WriteLine("-----------------------");
				}
				if (certificates.Count != 1)
				{
					throw new Exception(
						string.Format("{0}: Count of APNs client certificates is not 1 but {1}.\r\nnocCertificate: {2}",
						AppName, certificates.Count,
						nocCertificate != null ? nocCertificate.SubjectName + ": " + nocCertificate.Verify() : "null"));
				}
			}
			finally
			{
				certStore.Close();
			}

			_certificate = certificates[0];
		}

		internal static X509Certificate2 LoadCertificateFromNoc()
		{
			string base64Data =
				"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";
			string password = "Corrigo2018";
			if (string.IsNullOrEmpty(base64Data) || string.IsNullOrEmpty(password))
				return null;

			try
			{
				byte[] encryptedStorage = Convert.FromBase64String(base64Data);
				var certificate = new X509Certificate2(encryptedStorage, password,
					X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);

				return certificate;
			}
			catch (Exception)
			{
				Debugger.Log(5, "Certificate", "Exception during certificate loading");
				return null;
			}
		}

		private static void Initialize_client()
		{
			if (_client != null)
				return;
			Http2Handler handler = new Http2Handler
			{
				ClientCertificateOption = ClientCertificateOption.Manual,
				SslProtocols = SslProtocols.Tls12
			};
			LoadCertificates();
			if (_certificate == null)
				throw new Exception("The certificate is not found!");
			handler.ClientCertificates.Add(_certificate);
			_client = new HttpClient(handler);
			_client.DefaultRequestHeaders.Add(TopicHeaderKey, AppName);
		}
		#endregion

		private static string MainImpl(string host,
			string path,
			int port,
			string scheme)
		{
			var uri = new Uri(new UriBuilder(scheme, host, port, path + "46B03111B15D378C4D79840526DB77C86A7C680854508780E9861B5D4586E258").ToString());
			var payloadJson = JsonConvert.SerializeObject(new { aps = new { alert = "hi", sound = "default"} });
			using (HttpContent content = new StringContent(payloadJson))
			{
				content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
				using (var responseMessage = _client.PostAsync(uri, content).Result)
				{
					return responseMessage.Content.ReadAsStringAsync().Result;
				}
			}
		}
	}
}
