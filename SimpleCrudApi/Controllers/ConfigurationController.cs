using Microsoft.AspNetCore.Mvc;
using SimpleCrudApi.Utlilities;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        [HttpGet("{durationInMinutes}")]
        public IActionResult SimulateCpuUsageByCores(int durationInMinutes)
        {
            CpuStressSimulator.SimulateCpuUsageByCores(durationInMinutes);

            return Ok($"CPU stress test for {durationInMinutes} minute(s) complete.");
        }
    }
}
