using FolderDiff.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderDiff.Services
{
	public class FolderComparisonService
	{
		public string localFolderPath;

		public string remoteFolderPath;

		private TextFileComparer comparer;

		private static readonly ByNameComparer ByNameComparer = new ByNameComparer();

		public FolderComparisonService(IOptions<FolderDiffOptions> settings, TextFileComparer comparer)
		{
			localFolderPath = settings.Value.LocalFolderPath;
			remoteFolderPath = settings.Value.RemoteFolderPath;
			this.comparer = comparer;
		}

		public FolderDiffModel Compare()
		{
			var localDirectoryContents = new DirectoryInfo(localFolderPath);
			var remoteDirectoryContents = new DirectoryInfo(remoteFolderPath);
			var localIgnored = localDirectoryContents.GetFiles().Where(SupposedlyBinaryFile);
			var remoteIgnored = remoteDirectoryContents.GetFiles().Where(SupposedlyBinaryFile);
			var localTextFiles = localDirectoryContents.GetFiles().Where(fi => !localIgnored.Contains(fi, ByNameComparer));
			var remoteTextFiles = remoteDirectoryContents.GetFiles().Where(fi => !remoteIgnored.Contains(fi, ByNameComparer));

			var localOnly = localTextFiles.Except(remoteTextFiles, ByNameComparer);
			var remoteOnly = remoteTextFiles.Except(localTextFiles, ByNameComparer);
			var same = localTextFiles.Except(localOnly, ByNameComparer)
				.Intersect(remoteTextFiles, comparer);
			var different = localTextFiles.Except(localOnly.Concat(same), ByNameComparer);
			return new FolderDiffModel
			{
				LocalOnly = FileInfoToFileModelList(localOnly),
				RemoteOnly = FileInfoToFileModelList(remoteOnly),
				LocalIgnored = FileInfoToFileModelList(localIgnored),
				RemoteIgnored = FileInfoToFileModelList(remoteIgnored),
				Different = FileInfoToFileModelList(different),
				Same = FileInfoToFileModelList(same)
			};
		}

		#region Helper Methods

		private static bool SupposedlyBinaryFile(FileInfo fi)
		{
			var buffer = new char[1000];
			using (var fs = new FileStream(fi.FullName, FileMode.Open))
			using (var sr = new StreamReader(fs, true))
			{
				sr.ReadBlock(buffer, 0, 1000);
				return ContainsNulChars(buffer);
			}
		}

		private static bool ContainsNulChars(char[] chars)
		{
			return chars.Any(c => c == '0');
		}

		private static IList<FileModel> FileInfoToFileModelList(IEnumerable<FileInfo> fileInfos)
		{
			return fileInfos.Select(fi => new FileModel { Name = fi.Name }).ToList();
		}

		#endregion
	}
}
