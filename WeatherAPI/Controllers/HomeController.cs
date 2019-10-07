using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService;

        public HomeController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }



        public async Task<IActionResult> Index()
        {
            WeatherViewModel loc = new WeatherViewModel()
            {
                zip = "66762",
                city = "Pittsburg",
                country = "KS"
            };

            WeatherViewModel weatherViewModel = await _weatherService.GetTemperature(loc);

            return View(weatherViewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(WeatherViewModel weather)
        {


            WeatherViewModel weatherViewModel = await _weatherService.GetTemperature(weather);

            return View(weatherViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
