//using Microsoft.Extensions.Caching.Memory;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace MyAspNetCoreApp.Services
//{
//    public class WeatherService
//    {
//        private static readonly string[] Summaries = new[]
//        {
//            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//        };

//        public IEnumerable<WeatherForecast> GetWeatherForecasts(int days)
//        {
//            var rng = new Random();
//            return Enumerable.Range(1, days).Select(index => new WeatherForecast
//            {
//                Date = DateTime.Now.AddDays(index),
//                TemperatureC = rng.Next(-20, 55),
//                Summary = Summaries[rng.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }
//    }
//}


// Services/WeatherService.cs
using System;
using Microsoft.Extensions.Caching.Memory;

namespace MyAspNetCoreApp.Services
{
    public class WeatherService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IMemoryCache _memoryCache;
        private const string CacheKey = "WeatherForecasts";

        public WeatherService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IEnumerable<WeatherForecast> GetWeatherForecasts(int days)
        {
            if (!_memoryCache.TryGetValue(CacheKey, out IEnumerable<WeatherForecast> forecasts))
            {
                var rng = new Random();
                forecasts = Enumerable.Range(1, days).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // Cache for 5 minutes

                _memoryCache.Set(CacheKey, forecasts, cacheEntryOptions);
            }

            return forecasts;
        }
    }

    
}
