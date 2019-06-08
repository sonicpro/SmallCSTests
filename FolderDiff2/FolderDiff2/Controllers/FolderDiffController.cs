using FolderDiff2.Models;
using FolderDiff2.Services;
using Microsoft.AspNetCore.Mvc;

namespace FolderDiff2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FolderDiffController : ControllerBase
	{
		private readonly ApiHelperService apiService;
		private readonly FolderComparisonService comparisonService;

		public FolderDiffController(ApiHelperService apiService, FolderComparisonService comparisonService)
		{
			this.apiService = apiService;
			this.comparisonService = comparisonService;
		}

		[HttpGet()]
		public IActionResult Get()
		{
			return Ok(comparisonService.GetComparisonResult());
		}
	}
}
