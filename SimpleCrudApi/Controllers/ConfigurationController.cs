using Microsoft.AspNetCore.Mvc;
using SimpleCrudApi.Utilities;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        [HttpGet("{durationInMinutes}")]
        public IActionResult SimulateCpuUsageByCores(int durationInMinutes)
        {
            if (EnvironmentChecker.IsRunningInLambda())
            {
                return Ok("This functionality isn't available while running in Lambda.");
            }

            CpuStressSimulator.SimulateCpuUsageByCores(durationInMinutes);

            return Ok($"CPU stress test for {durationInMinutes} minute(s) complete.");
        }
    }
}
