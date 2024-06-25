using System.Text.Json.Serialization;

namespace SimpleCrudApi.Models
{
    public class AWSRegionAndAZInfo
    {
        [JsonPropertyName("region"), JsonRequired]
        public string Region { get; set; }

        [JsonPropertyName("availabilityZone"), JsonRequired]
        public string AvailabilityZone { get; set; }
    }
}
