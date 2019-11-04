using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Services
{
    public interface IDataService
    {
        Task<string> GetDataAsString(string url);
    }
}
