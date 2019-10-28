using System.Text.Json.Serialization;

namespace MyFirstWebApi.Models.OpenWeatherMap
{
    public class Clouds
    {
        [JsonPropertyName("all")]
        public int Cloudiness { get; set; }
    }
}