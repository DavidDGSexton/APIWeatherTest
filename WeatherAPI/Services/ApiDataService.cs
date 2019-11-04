using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherAPI.Services
{
    public class ApiDataService : IDataService
    {
        private HttpClient _httpClient;

        public ApiDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<string> GetDataAsString(string url)
        {
            return _httpClient.GetStringAsync(url);
        }
    }
}
