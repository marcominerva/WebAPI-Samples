using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyFirstWebApi.Models.OpenWeatherMap
{
    public class ForecastWeatherData
    {
        [JsonPropertyName("dt")]
        [JsonConverter(typeof(UnixToDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonPropertyName("main")]
        public ForecastWeatherDetail WeatherDetail { get; set; }

        [JsonPropertyName("weather")]
        public IEnumerable<WeatherInfo> WeatherInfo { get; set; }

        [JsonPropertyName("clouds")]
        public Clouds Clouds { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }
    }
}