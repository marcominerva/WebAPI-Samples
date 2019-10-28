using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyFirstWebApi.Models.OpenWeatherMap
{
    public class ForecastWeather
    {
        [JsonPropertyName("city")]
        public ForecastCity City { get; set; }

        [JsonPropertyName("cod")]
        public string Code { get; set; }

        [JsonPropertyName("list")]
        public IEnumerable<ForecastWeatherData> WeatherData { get; set; }
    }
}