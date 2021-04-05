using System.Text.Json.Serialization;

namespace MyFirstWebApi.Shared.OpenWeatherMap
{
    public class Clouds
    {
        [JsonPropertyName("all")]
        public int Cloudiness { get; set; }
    }
}