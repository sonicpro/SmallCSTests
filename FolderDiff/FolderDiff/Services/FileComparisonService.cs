using FolderDiff.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderDiff.Services
{
	public class FileComparisonService
	{
		private string localFolderPath;

		private string remoteFolderPath;

		public FileComparisonService(IOptions<FolderDiffOptions> settings)
		{
			localFolderPath = settings.Value.LocalFolderPath;
			remoteFolderPath = settings.Value.RemoteFolderPath;
		}

		public FileDiffModel GetFileDifference(string fileName)
		{
			var local = Directory.GetFiles(localFolderPath);
			var remote = Directory.GetFiles(remoteFolderPath);

			var localFilePath = local.FirstOrDefault(n => Path.GetFileName(n).Equals(fileName, StringComparison.OrdinalIgnoreCase));
			var remoteFilePath = remote.FirstOrDefault(n => Path.GetFileName(n).Equals(fileName, StringComparison.OrdinalIgnoreCase));

			if (localFilePath == null || remoteFilePath == null)
			{
				throw new InvalidOperationException();
			}

			int firstDifferentLineIndex = GetFirstDiscrepancyIndex(localFilePath, remoteFilePath);
			var localFileContents = File.ReadAllLines(localFilePath);
			var remoteFileContents = File.ReadAllLines(remoteFilePath);
			return new FileDiffModel
			{
				Name = fileName,
				LocalFirstDifferentLineIndex = (localFileContents.Length - 1) < firstDifferentLineIndex ? -1 : firstDifferentLineIndex,
				RemoteFirstDifferentLineIndex = (remoteFileContents.Length - 1) < firstDifferentLineIndex ? -1 : firstDifferentLineIndex,
				LocalFirstDifferentText = (localFileContents.Length - 1) < firstDifferentLineIndex ? string.Empty :
					localFileContents[firstDifferentLineIndex],
				RemoteFirstDifferentText = (remoteFileContents.Length - 1) < firstDifferentLineIndex ? string.Empty :
					remoteFileContents[firstDifferentLineIndex]
			};
		}

		#region Helper Methods

		private int GetFirstDiscrepancyIndex(string firstFilePath, string secondFilePath)
		{
			var isFirstFileShorter = File.ReadAllLines(firstFilePath).Length < File.ReadAllLines(secondFilePath).Length;
			IEnumerable<string> longerContents = File.ReadLines(isFirstFileShorter ? secondFilePath : firstFilePath);
			IEnumerable<string> shorterContents = File.ReadLines(isFirstFileShorter ? firstFilePath : secondFilePath);

			int differentLineIndex = 0;
			if (shorterContents.Where((line, i) =>
			{
				differentLineIndex = i;
				return longerContents.Skip(i).First() != line;
			}).Any())
			{
				return differentLineIndex;
			}
			return ++differentLineIndex;
		}

		#endregion
	}
}
