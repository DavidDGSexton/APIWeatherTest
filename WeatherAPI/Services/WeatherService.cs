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
        private IDataService _dataService;
        private string _apiKey = "&APPID=0c549dab70a57c716eefcb677cd93cde";
        public WeatherService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<WeatherViewModel> GetWeather(WeatherViewModel locOfWeather)
        {
            WeatherViewModel weatherResult = null;

            try
            {
                var content =
                await _dataService.GetDataAsString("http://api.openweathermap.org/data/2.5/weather?zip=" +
                                                           locOfWeather.zip + _apiKey);
                var weather = JsonConvert.DeserializeObject<RootWeather>(content);


                WeatherViewModel weatherViewModel = new WeatherViewModel()
                {
                    temp = KelvinToFahrenheit(weather.main.temp),
                    temp_max = KelvinToFahrenheit(weather.main.temp_max),
                    temp_min = KelvinToFahrenheit(weather.main.temp_min),
                    weatherDescription = weather.weather.FirstOrDefault(x => x != null).description,
                    city = weather.name,
                    country = weather.sys.country

                };

                weatherResult = weatherViewModel;            
            }

            catch 
            {

                throw new Exception("Bad Zipcode");
            }

            

            return weatherResult;
        }

        public string KelvinToFahrenheit(double temp)
        {
            return ((temp * 9 / 5) - 459.67).ToString("#") + " °F";
        }

    }
}
