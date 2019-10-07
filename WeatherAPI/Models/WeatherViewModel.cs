using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    public class WeatherViewModel
    {
        public string temp { get; set; }
        public string temp_min { get; set; }
        public string temp_max { get; set; }

        public string weatherDescription { get; set; }


        public string city { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
    }
}
