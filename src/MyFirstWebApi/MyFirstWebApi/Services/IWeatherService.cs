using MyFirstWebApi.Shared.OpenWeatherMap;
using System.Threading.Tasks;

namespace MyFirstWebApi.Services
{
    public interface IWeatherService
    {
        Task<CurrentWeather> GetCurrentWeatherAsync(string country, string zipCode);

        Task<ForecastWeather> GetForecastWeatherAsync(string country, string zipCode);
    }
}