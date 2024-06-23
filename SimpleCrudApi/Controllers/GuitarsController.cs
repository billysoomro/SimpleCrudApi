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
            var conditions = new List<ScanCondition>();
            var guitars = await _dynamoDBContext.ScanAsync<Guitar>(conditions).GetRemainingAsync();

            return Ok(guitars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var guitar = await _dynamoDBContext.LoadAsync<Guitar>(id);

            return Ok(guitar);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Guitar guitar)
        {
            await _dynamoDBContext.SaveAsync(guitar);

            return Created($"api/guitars/{guitar.Id}", guitar);           
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Guitar guitar)
        {
            var guitarFromTable = await _dynamoDBContext.LoadAsync<Guitar>(guitar.Id);

            guitarFromTable.Make = guitar.Make;
            guitarFromTable.Model = guitar.Model;
            guitarFromTable.Shape = guitar.Shape;
            guitarFromTable.Strings = guitar.Strings;

            await _dynamoDBContext.SaveAsync(guitarFromTable);

            return Ok(guitar);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dynamoDBContext.DeleteAsync<Guitar>(id);

            return Ok($"Guitar {id} Deleted");
        }
    }
}
