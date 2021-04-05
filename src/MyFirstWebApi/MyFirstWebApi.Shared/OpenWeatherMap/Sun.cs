using MyFirstWebApi.Shared.Converters;
using System;
using System.Text.Json.Serialization;

namespace MyFirstWebApi.Shared.OpenWeatherMap
{
    public class Sun
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonConverter(typeof(UnixToDateTimeConverter))]
        [JsonPropertyName("sunrise")]
        public DateTime Sunrise { get; set; }

        [JsonConverter(typeof(UnixToDateTimeConverter))]
        [JsonPropertyName("sunset")]
        public DateTime Sunset { get; set; }
    }
}