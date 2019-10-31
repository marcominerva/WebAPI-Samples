using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MyFirstWebApi.Models;
using MyFirstWebApi.Shared.OpenWeatherMap;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyFirstWebApi.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMemoryCache cache;
        private readonly AppSettings settings;

        public WeatherService(IHttpClientFactory httpClientFactory, IMemoryCache cache, IOptions<AppSettings> settings)
        {
            this.httpClientFactory = httpClientFactory;
            this.cache = cache;
            this.settings = settings.Value;
        }

        public async Task<CurrentWeather> GetCurrentWeatherAsync(string country, string zipCode)
        {
            var cacheKey = $"current-{country}-{zipCode}";
            var weather = await cache.GetOrCreateAsync(cacheKey, async (entry) =>
            {
                var data = await GetWeatherDataAsync<CurrentWeather>("weather", country, zipCode);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);

                return data;
            });

            return weather;
        }

        public async Task<ForecastWeather> GetForecastWeatherAsync(string country, string zipCode)
        {
            var cacheKey = $"forecast-{country}-{zipCode}";
            var weather = await cache.GetOrCreateAsync(cacheKey, async (entry) =>
            {
                var data = await GetWeatherDataAsync<ForecastWeather>("forecast", country, zipCode);
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);

                return data;
            });

            return weather;
        }

        private async Task<T> GetWeatherDataAsync<T>(string type, string country, string zipCode)
        {
            var url = $"https://api.openweathermap.org/data/2.5/{type}?zip={zipCode},{country}&units=metric&APPID={settings.OpenWeatherMapAppId}";

            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var content = JsonSerializer.Deserialize<T>(jsonResponse);

            return content;
        }
    }
}
