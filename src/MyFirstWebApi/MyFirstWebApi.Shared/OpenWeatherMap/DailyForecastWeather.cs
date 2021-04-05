using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyFirstWebApi.Shared.OpenWeatherMap
{
    public class DailyForecastWeather
    {
        [JsonPropertyName("city")]
        public ForecastCity City { get; set; }

        [JsonPropertyName("cod")]
        public string Code { get; set; }

        [JsonPropertyName("list")]
        public IEnumerable<DailyForecastWeatherData> WeatherData { get; set; }
    }
}
