namespace SimpleCrudApi.Utlilities
{
    public class InstanceMetadataRetriever
    {
        private static readonly HttpClient client = new HttpClient();
        private const string InstanceMetadataUrl = "http://169.254.169.254/latest/meta-data/";

        public string Region { get; private set; }
        public Task<string> AvailabilityZone { get; private set; }

        public InstanceMetadataRetriever()
        {
            Region = GetRegion();
            AvailabilityZone = GetAvailabilityZoneAsync();
        }

        private string GetRegion()
        {
            // Use the AWS_REGION environment variable if available
            var region = Environment.GetEnvironmentVariable("AWS_REGION");

            if (string.IsNullOrEmpty(region))
            {
                // Fallback to the instance metadata service
                region = GetMetadata("placement/region");
            }

            return region;
        }

        private async Task<string> GetAvailabilityZoneAsync()
        {
            return await GetMetadataAsync("placement/availability-zone");
        }

        private string GetMetadata(string path)
        {
            try
            {
                return client.GetStringAsync(InstanceMetadataUrl + path).Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving metadata: {ex.Message}");
            }
        }

        private async Task<string> GetMetadataAsync(string path)
        {
            try
            {
                var response = await client.GetAsync(InstanceMetadataUrl + path);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving metadata: {ex.Message}");
            }
        }
    }
}
