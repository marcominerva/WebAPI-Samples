using System.Text.Json.Serialization;

namespace MyFirstWebApi.Shared.OpenWeatherMap
{
    public class WeatherInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main")]
        public string Condition { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string ConditionIcon { get; set; }

        public string ConditionIconUrl => $"https://openweathermap.org/img/w/{ConditionIcon}.png";
    }
}