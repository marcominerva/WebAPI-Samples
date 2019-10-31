using System.Text.Json.Serialization;

namespace MyFirstWebApi.Shared.OpenWeatherMap
{
    public class Temperature
    {
        [JsonPropertyName("day")]
        public decimal Day { get; set; }

        [JsonPropertyName("min")]
        public decimal Min { get; set; }

        [JsonPropertyName("max")]
        public decimal Max { get; set; }

        [JsonPropertyName("night")]
        public decimal Night { get; set; }

        [JsonPropertyName("eve")]
        public decimal Evening { get; set; }

        [JsonPropertyName("morn")]
        public decimal Morning { get; set; }
    }
}
