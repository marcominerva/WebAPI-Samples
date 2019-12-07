using MyFirstWebApi.Shared.OpenWeatherMap;
using System.Threading.Tasks;

namespace WeatherClient.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherServiceApi weatherServiceApi;

        public WeatherService(IWeatherServiceApi weatherServiceApi)
        {
            this.weatherServiceApi = weatherServiceApi;
        }

        public Task<CurrentWeather> GetCurrentWeatherAsync(string country, string zipCode)
        {
            return weatherServiceApi.GetCurrentWeatherAsync(country, zipCode);
        }

        public Task<ForecastWeather> GetForecastWeatherAsync(string country, string zipCode)
        {
            return weatherServiceApi.GetForecastWeatherAsync(country, zipCode);
        }
    }
}
