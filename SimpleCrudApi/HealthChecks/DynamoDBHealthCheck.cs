using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SimpleFrontEnd.HealthChecks
{
    public class DynamoDBHealthCheck : IHealthCheck
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;

        public DynamoDBHealthCheck(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new ListTablesRequest
                {
                    Limit = 1
                };

                var response = await _dynamoDbClient.ListTablesAsync(request, cancellationToken);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new HealthCheckResult(HealthStatus.Healthy, "DynamoDB is reachable.");
                }

                return new HealthCheckResult(HealthStatus.Unhealthy, "DynamoDB is not reachable.");
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, "DynamoDB health check failed.", ex);               
            }
        }
    }
}
