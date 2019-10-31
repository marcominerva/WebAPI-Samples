using Microsoft.Extensions.Options;
using MyFirstWebApi.Shared.OpenWeatherMap;
using Refit;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherClient.Models;

namespace WeatherClient.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherServiceApi weatherServiceApi;

        public WeatherService(IOptions<AppSettings> appSettings)
        {
            // Create a RestClient using Refit and the System.Text.Json serializer.
            // See the JsonContentSerializer.cs class for implementation details.
            var settings = new RefitSettings
            {
                ContentSerializer = new JsonContentSerializer(new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                })
            };

            weatherServiceApi = RestService.For<IWeatherServiceApi>(appSettings.Value.ServiceUrl, settings);
        }

        public Task<CurrentWeather> GetCurrentWeatherAsync(string country, string zipCode)
        {
            return weatherServiceApi.GetCurrentWeather(country, zipCode);
        }

        public Task<ForecastWeather> GetForecastWeatherAsync(string country, string zipCode)
        {
            return weatherServiceApi.GetForecastWeather(country, zipCode);
        }
    }
}
