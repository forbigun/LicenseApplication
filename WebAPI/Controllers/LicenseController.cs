using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LicenseController : ControllerBase
	{
		private readonly LicenseService _service;

		public LicenseController(LicenseService service)
		{
			_service = service;
		}

		[HttpPost("register")]
		public IActionResult RegisterKey(RegisterKeyRequest request)
		{
			if (request.Pass != "root")
			{
				return BadRequest();
			}

			_service.RegisterKey(request.Key);

			return Ok();
		}

		[HttpGet("is-active/{key}")]
		public bool IsActive(string key) => _service.KeyIsActive(key);
	}
}