using System.Text.Json.Serialization;

namespace MyFirstWebApi.Models.OpenWeatherMap
{
    public class ForecastWeatherDetail
    {
        [JsonPropertyName("temp")]
        public decimal Temperature { get; set; }

        [JsonPropertyName("pressure")]
        public double Pressure { get; set; }

        [JsonPropertyName(("sea_level"))]
        public double SeaLevel { get; set; }

        [JsonPropertyName("grnd_level")]
        public double GroundLevel { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }
}