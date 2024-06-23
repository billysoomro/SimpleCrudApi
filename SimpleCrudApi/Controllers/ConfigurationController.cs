using Microsoft.AspNetCore.Mvc;
using SimpleCrudApi.Utlilities;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        [HttpPost("SimulateCpuUsage")]
        public IActionResult SimulateCpuUsage([FromQuery] int durationInMinutes)
        {
            CpuStressSimulator.SimulateCpuUsage(durationInMinutes);

            return Ok($"CPU stress test for {durationInMinutes} minute(s) comlete.");
        }

        [HttpPost("SimulateCpuUsageByCores")]
        public IActionResult SimulateCpuUsageByCores([FromQuery] int durationInMinutes)
        {
            CpuStressSimulator.SimulateCpuUsageByCores(durationInMinutes);

            return Ok($"CPU stress test for {durationInMinutes} minute(s) comlete.");
        }
    }
}
