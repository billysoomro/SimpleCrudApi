using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using SimpleCrudApi.Models;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuitarsController : ControllerBase
    {
        private readonly IDynamoDBContext _dynamoDBContext;

        public GuitarsController(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var conditions = new List<ScanCondition>();
                var guitars = await _dynamoDBContext.ScanAsync<Guitar>(conditions).GetRemainingAsync();

                if (guitars == null)
                {
                    return NotFound("No Guitars found");
                }

                return Ok(guitars);
            }

            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var guitar = await _dynamoDBContext.LoadAsync<Guitar>(id);

                if (guitar == null)
                {
                    return NotFound($"Guitar id {id} not found");
                }

                return Ok(guitar);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Guitar guitar)
        {
            try
            {
                await _dynamoDBContext.SaveAsync(guitar);

                return Created($"api/guitars/{guitar.Id}", guitar);
            }

            catch (Exception ex) { 
            
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Guitar guitar)
        {
            try
            {
                await _dynamoDBContext.SaveAsync(guitar);

                return Ok(guitar);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dynamoDBContext.DeleteAsync<Guitar>(id);

            return Ok($"Guitar {id} Deleted");
        }
    }
}
