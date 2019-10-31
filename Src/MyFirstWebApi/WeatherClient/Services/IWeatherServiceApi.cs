using MyFirstWebApi.Shared.OpenWeatherMap;
using Refit;
using System.Threading.Tasks;

namespace WeatherClient.Services
{
    public interface IWeatherServiceApi
    {
        [Get("/Weather/{country}/{zipCode}/current")]
        Task<CurrentWeather> GetCurrentWeather(string country, string zipCode);

        [Get("/Weather/{country}/{zipCode}/forecast")]
        Task<ForecastWeather> GetForecastWeather(string country, string zipCode);
    }
}
