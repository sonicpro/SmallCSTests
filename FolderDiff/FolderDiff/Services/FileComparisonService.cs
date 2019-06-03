using FolderDiff.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FolderDiff.Services
{
	public class FileComparisonService
	{
		private string localFolderPath;

		private string remoteFolderPath;

		private bool ignoreEncoding;

		public FileComparisonService(IOptions<FolderDiffOptions> settings)
		{
			localFolderPath = settings.Value.LocalFolderPath;
			remoteFolderPath = settings.Value.RemoteFolderPath;
			ignoreEncoding = settings.Value.IgnoreEncoding;
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

			var result = new FileDiffModel
			{
				Name = fileName,
				RemoteDetectedEncoding = DetectEncoding(remoteFilePath),
				LocalDetectedEncoding = DetectEncoding(localFilePath)
			};

			var localFileDifferentText = string.Empty;
			var remoteFileDifferentText = string.Empty;
			var localFileContents = File.ReadAllLines(localFilePath);
			var remoteFileContents = File.ReadAllLines(remoteFilePath);
			var differentLineIndex = 0;

			if (ignoreEncoding || result.RemoteDetectedEncoding.GetType() == result.LocalDetectedEncoding.GetType())
			{
				differentLineIndex = GetFirstDiscrepancyIndex(localFilePath, remoteFilePath);
			}

			if (differentLineIndex < localFileContents.Length)
			{
				localFileDifferentText = localFileContents[differentLineIndex];
			}
			if (differentLineIndex < remoteFileContents.Length)
			{
				remoteFileDifferentText = remoteFileContents[differentLineIndex];
			}

			result.DifferentLineIndex = differentLineIndex;
			result.LocalFirstDifferentLineText = localFileDifferentText;
			result.RemoteFirstDifferentLineText = remoteFileDifferentText;
			return result;
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

		private static Encoding DetectEncoding(string fullName)
		{
			var buffer = new char[1000];
			using (var fs = new FileStream(fullName, FileMode.Open, FileAccess.Read))
			using (var sr = new StreamReader(fs, true))
			{
				sr.ReadBlock(buffer, 0, 1000);
				return sr.CurrentEncoding;
			}
		}

		#endregion
	}
}
