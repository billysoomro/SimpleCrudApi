using Microsoft.AspNetCore.Mvc;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanaryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("The SimpleCrudApi is running.");
        }
    }
}
