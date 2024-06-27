namespace SimpleCrudApi.Utilities
{
    public static class InstanceMetadataRetriever
    {
        private static readonly HttpClient client = new HttpClient();
        private const string InstanceMetadataUrl = "http://169.254.169.254/latest/meta-data/";

        public static async Task<string> GetAvailabilityZoneAsync()
        {
            return await GetMetadataAsync("placement/availability-zone");
        }

        private static async Task<string> GetMetadataAsync(string path)
        {
            try
            {
                var token = await GetImdsV2TokenAsync();
                var metadataUrl = $"http://169.254.169.254/latest/meta-data/{path}";
                var request = new HttpRequestMessage(HttpMethod.Get, metadataUrl);

                request.Headers.Add("X-aws-ec2-metadata-token", token);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var metadata = await response.Content.ReadAsStringAsync();

                return metadata;
            }

            catch (Exception ex)
            {
                throw new Exception($"Error retrieving metadata: {ex.Message}");
            }
        }

        private static async Task<string> GetImdsV2TokenAsync()
        {
            var tokenUrl = "http://169.254.169.254/latest/api/token";
            var ttlSeconds = 21600;
            var request = new HttpRequestMessage(HttpMethod.Put, tokenUrl);

            request.Headers.Add("X-aws-ec2-metadata-token-ttl-seconds", ttlSeconds.ToString());

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var token = await response.Content.ReadAsStringAsync();

            return token;
        }
    }
}
