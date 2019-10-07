using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public class WeatherService : IWeatherService
    {
        private static HttpClient _httpClient;
        private string _apiKey = "&APPID=0c549dab70a57c716eefcb677cd93cde";
        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);

        }

        public async Task<WeatherViewModel> GetTemperature(WeatherViewModel locOfWeather)
        {
            var response = await _httpClient.GetAsync("?zip=" + locOfWeather.zip + _apiKey);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var weather = JsonConvert.DeserializeObject<RootWeather>(content);

            WeatherViewModel weatherViewModel = new WeatherViewModel()
            {
                temp = ((weather.main.temp * 9 / 5) - 459.67).ToString("#") + " °F",
                temp_max = ((weather.main.temp_max * 9 / 5) - 459.67).ToString("#") + " °F",
                temp_min = ((weather.main.temp_min * 9 / 5) - 459.67).ToString("#") + " °F",
                weatherDescription = weather.weather.FirstOrDefault(x => x != null).description,
                city = weather.name,
                country = weather.sys.country
               
            };

            

            return weatherViewModel;
        }         

    }
}
