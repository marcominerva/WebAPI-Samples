using MyFirstWebApi.Shared.OpenWeatherMap;
using Refit;
using System.Threading.Tasks;

namespace WeatherClient.Services
{
    public interface IWeatherServiceApi
    {
        [Get("/Weather/{country}/{zipCode}/current")]
        Task<CurrentWeather> GetCurrentWeatherAsync(string country, string zipCode);

        [Get("/Weather/{country}/{zipCode}/forecast")]
        Task<ForecastWeather> GetForecastWeatherAsync(string country, string zipCode);
    }
}
