using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq.Language.Flow;
using WeatherAPI.Models;
using WeatherAPI.Services;
using Xunit;
using Newtonsoft.Json;


namespace UnitTestsWeatherAPI
{
    [TestClass]
    public class WeatherServiceTests
    {
        [Fact]
        public async void Services_WeatherService_GetWeather_ResultIsWeather()
        {
            // ARRANGE
            Mock<IDataService> dataServiceMock = new Mock<IDataService>();

            dataServiceMock.Setup(x => x.GetDataAsString(It.IsAny<string>()))
                .ReturnsAsync("{\"coord\":{\"lon\":-94.31,\"lat\":37.16},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"base\":\"stations\",\"main\":{\"temp\":289.89,\"pressure\":1017,\"humidity\":48,\"temp_min\":288.71,\"temp_max\":290.93},\"visibility\":16093,\"wind\":{\"speed\":2.02,\"deg\":347},\"clouds\":{\"all\":1},\"dt\":1572895681,\"sys\":{\"type\":1,\"id\":4590,\"country\":\"US\",\"sunrise\":1572871467,\"sunset\":1572909427},\"timezone\":-21600,\"id\":0,\"name\":\"Carthage\",\"cod\":200}");

            var subjectUnderTest = new WeatherService(dataServiceMock.Object);

            WeatherViewModel weatherViewModel = new WeatherViewModel()
            {
                city = "test",
                country = "test",
                zip = "74737"

            };

            // ACT
            var result = await subjectUnderTest
                   .GetWeather(weatherViewModel);

            // ASSERT
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Carthage", result.city);
            Xunit.Assert.Equal("US", result.country);
            Xunit.Assert.Equal("62 °F", result.temp);
            Xunit.Assert.Equal("64 °F", result.temp_max);
            Xunit.Assert.Equal("60 °F", result.temp_min);
            Xunit.Assert.Equal("clear sky", result.weatherDescription);

        }
    }
}
