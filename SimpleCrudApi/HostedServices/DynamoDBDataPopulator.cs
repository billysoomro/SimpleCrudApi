using Amazon.DynamoDBv2.DataModel;
using SimpleCrudApi.Models;

namespace SimpleCrudApi.HostedServices
{
    public class DynamoDBDataPopulator : IHostedService
    {
        private readonly IDynamoDBContext _dynamoDBContext;

        public DynamoDBDataPopulator(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var conditions = new List<ScanCondition>();
            var guitarsFromTable = await _dynamoDBContext.ScanAsync<Guitar>(conditions).GetRemainingAsync();

            if (guitarsFromTable.Count == 0)
            {
                var guitars = PopulateGuitarList();

                foreach (var guitar in guitars)
                {
                    await _dynamoDBContext.SaveAsync(guitar, cancellationToken);
                }
            }            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private List<Guitar> PopulateGuitarList()
        {
            return new List<Guitar>
            {
                new Guitar(1, "PRS" , "Tremonti Signature Model", "Solid body", 6),
                new Guitar(2, "Gibson" , "Les Paul", "Solid body", 6),
                new Guitar(3, "Fender" , "Stratocaster", "Solid body", 6),
                new Guitar(4, "Ibanez" , "RG Series", "Solid body", 7),
                new Guitar(5,"ESP" , "KH-2 VINTAGE", "Solid body", 6)
            };
        }
    }
}
