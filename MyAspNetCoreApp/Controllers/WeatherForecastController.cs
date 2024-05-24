// Controllers/WeatherForecastController.cs
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Services;

namespace MyAspNetCoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherForecastController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Get(int days = 5)
        {
            var forecasts = _weatherService.GetWeatherForecasts(days);
            return Ok(forecasts);
        }
    }
}
