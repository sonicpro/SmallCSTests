using FolderDiff.Models;
using FolderDiff.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FolderDiff.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FileDiffController : ControllerBase
	{
		private FolderComparisonService folderComparisonService;
		private FileComparisonService fileComparisonService;
		private bool ignoreEncoding;

		public FileDiffController(FolderComparisonService folderComparisonService,
			FileComparisonService fileComparisonService,
			IOptions<FolderDiffOptions> settings)
		{
			this.folderComparisonService = folderComparisonService;
			this.fileComparisonService = fileComparisonService;
			this.ignoreEncoding = settings.Value.IgnoreEncoding;
		}

		[HttpGet("{fileName}")]
		public IActionResult GetFirstDiscrepancyInTheFile(string fileName)
		{
			var folderDiffModel = folderComparisonService.Compare();
			var fileModel = folderDiffModel.Different.FirstOrDefault(f => f.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase));

			// Any fileName but the ones considered "different" produces 404 "NotFound" response.
			if (fileModel == null)
			{
				return NotFound();
			}

			if (!ignoreEncoding && fileModel.LocalDetectedEncoding.GetType() != fileModel.RemoteDetectedEncoding.GetType())
			{
				var result = new FileDiffModel
				{
					Name = fileModel.Name,
					LocalDetectedEncoding = fileModel.LocalDetectedEncoding,
					RemoteDetectedEncoding = fileModel.RemoteDetectedEncoding,
					LocalFirstDifferentLineIndex = 0,
					RemoteFirstDifferentLineIndex = 0,
					LocalFirstDifferentText = fileModel.LocalFileFirstLine,
					RemoteFirstDifferentText = fileModel.RemoteFileFirstLine
				};
			}

			try
			{
				var result = fileComparisonService.GetFileDifference(fileName);
				result.LocalDetectedEncoding = fileModel.LocalDetectedEncoding;
				result.RemoteDetectedEncoding = fileModel.RemoteDetectedEncoding;
				return Ok(result);
			}
			catch
			{
				return Conflict();
			}
		}
	}
}
