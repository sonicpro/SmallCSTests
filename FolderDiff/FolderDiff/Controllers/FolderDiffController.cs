using Microsoft.AspNetCore.Mvc;
using FolderDiff.Services;

namespace FolderDiff.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FolderDiffController : ControllerBase
	{
		private FolderComparisonService comparisonService;

		public FolderDiffController(FolderComparisonService comparisonService)
		{
			this.comparisonService = comparisonService;
		}

		[HttpGet("configuration")]
		public ActionResult<string> GetConfiguration()
		{
			return new ActionResult<string>($"RemoteFolder: {comparisonService.RemoteFolderPath}; " +
				$"LocalFolder: {comparisonService.LocalFolderPath}");
		}
	}
}
