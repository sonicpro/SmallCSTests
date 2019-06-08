using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FolderDiff2
{
	public class ApiOptions
	{
		public string Host { get; set; }

		public string HttpScheme { get; set; }

		public int Port { get; set; }

		public string AuthorizationScheme { get; set; }

		public string Path { get; set; }
	}
}
