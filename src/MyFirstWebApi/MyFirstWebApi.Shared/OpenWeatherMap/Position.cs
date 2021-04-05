using System.Text.Json.Serialization;

namespace MyFirstWebApi.Shared.OpenWeatherMap
{
    public class Position
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
    }
}