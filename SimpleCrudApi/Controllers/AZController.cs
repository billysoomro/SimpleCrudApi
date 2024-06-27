using Microsoft.AspNetCore.Mvc;
using SimpleCrudApi.Utilities;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AZController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var availabilityZone = await InstanceMetadataRetriever.GetAvailabilityZoneAsync();

                return Ok(availabilityZone);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
