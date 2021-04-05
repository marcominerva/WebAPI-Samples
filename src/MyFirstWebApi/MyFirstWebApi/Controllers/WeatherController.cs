using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstWebApi.Services;
using MyFirstWebApi.Shared.OpenWeatherMap;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MyFirstWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService weatherService;
        private readonly ILogger<WeatherController> logger;

        public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
        {
            this.logger = logger;
            this.weatherService = weatherService;
        }

        [HttpGet]
        [Route("{country}/{zipCode}/current")]
        public async Task<ActionResult<CurrentWeather>> GetCurrentWeather(string country, string zipCode)
        {
            var weather = await weatherService.GetCurrentWeatherAsync(country, zipCode);
            if (weather == null)
            {
                return NotFound();
            }

            return weather;
        }

        [HttpGet]
        [Route("{country}/{zipCode}/forecast")]
        public async Task<ActionResult<ForecastWeather>> GetForecastWeather(string country, string zipCode)
        {
            var weather = await weatherService.GetForecastWeatherAsync(country, zipCode);
            if (weather == null)
            {
                return NotFound();
            }

            return weather;
        }
    }
}
