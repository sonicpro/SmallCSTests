using Microsoft.Extensions.Options;

namespace FolderDiff.Services
{
	public class FolderComparisonService
	{
		public string LocalFolderPath { get; set; }

		public string RemoteFolderPath { get; set; }

		public FolderComparisonService(IOptions<FolderOptions> opt)
		{
			LocalFolderPath = opt.Value.LocalFolderPath;
			RemoteFolderPath = opt.Value.RemoteFolderPath;
		}
	}
}
