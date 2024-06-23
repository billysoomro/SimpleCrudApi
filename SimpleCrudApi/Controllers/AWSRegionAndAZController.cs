using Microsoft.AspNetCore.Mvc;
using SimpleCrudApi.Models;
using SimpleCrudApi.Utlilities;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AWSRegionAndAZController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var instanceMetadata = new InstanceMetadataRetriever();
            var awsRegionAndAzInfo = new AWSRegionAndAZInfo
            {
                Region = instanceMetadata.Region,
                AvailabilityZone = await instanceMetadata.AvailabilityZone
            };

            return Ok(awsRegionAndAzInfo);
        }
    }
}
