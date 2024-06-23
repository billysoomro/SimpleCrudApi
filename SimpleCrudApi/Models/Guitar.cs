using System.Text.Json.Serialization;

namespace SimpleCrudApi.Models
{
    public class Guitar
    {
        [JsonPropertyName("id"), JsonRequired]
        public int Id { get; set; }

        [JsonPropertyName("make"), JsonRequired]
        public string Make { get; set; }

        [JsonPropertyName("model"), JsonRequired]
        public string Model { get; set; }

        [JsonPropertyName("shape"), JsonRequired]
        public string Shape { get; set; }

        [JsonPropertyName("strings"), JsonRequired]
        public int Strings { get; set; }

        public Guitar(int id, string make, string model, string shape, int strings)
        {
            Id = id;
            Make = make;
            Model = model;
            Shape = shape;
            Strings = strings;
        }

        public Guitar()
        { 
        }
    }
}
