using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kontrolmatik.Models;
using Kontrolmatik.Data;
using Kontrolmatik.Services;
using Microsoft.AspNetCore.Authorization;

namespace Kontrolmatik.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly WeatherDbContext dbContext;
        private readonly IWeatherService weatherService;
        public HomeController(WeatherDbContext dbContext, IWeatherService weatherService)
        {
            this.dbContext = dbContext;
            this.weatherService = weatherService;
        }

        public IActionResult Index()
        {
            var weathers = dbContext.Weathers.OrderByDescending(x => x.CreatedAt).ToList();
            return View(weathers);
        }

        public async Task<IActionResult> Refresh()
        {
            var currentWeather = await weatherService.GetWeatherInfo("Istanbul", "TR");
            dbContext.Weathers.Add(new Data.Tables.Weather()
            {
                CityName = currentWeather.name,
                CountryCode = currentWeather.sys.country,
                Temperature = (decimal)currentWeather.main.temp,
                TemperatureMax = (decimal)currentWeather.main.temp_max,
                TemperatureMin = (decimal)currentWeather.main.temp_min,
                WeatherStatus = currentWeather.weather[0].main,
                WeatherStatusDescription = currentWeather.weather[0].description,
                CreatedAt = DateTime.Now
            });
            try
            {
                dbContext.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
