using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WeatherAPI.Controllers;
using WeatherAPI.Models;
using WeatherAPI.Services;
using Xunit;
using AutoFixture;
using System.Web.Mvc;
using System.Threading.Tasks;
using System;

namespace UnitTestsWeatherAPI
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly IFixture _fixture;

        public HomeControllerTests()
        {
            _fixture = new FixtureFactory().WithDefaults().Create();
        }

        [Fact]
        public async void Controllers_Home_PostIndexSuccess_ReturnsViewResult()
        {
            // ARRANGE
            

            var sut = _fixture.Create<HomeController>();
            var weather = _fixture.Create<Task<WeatherViewModel>>();
            var weatherSubmit = _fixture.Create<WeatherViewModel>();
            WeatherViewModel weatherViewModel = new WeatherViewModel()
            {
                city = "test",
                country = "test",
                zip = "74737"

            };

            Mock<IWeatherService> weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(x => x.GetWeather(It.IsAny<WeatherViewModel>()))
              .Returns(weather);



            // ACT
            var result = await sut.Index(weatherSubmit);
                   

            // ASSERT
            Xunit.Assert.NotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.ViewResult));

        }

        [Fact]
        public async void Controllers_Home_PostIndexFailure_ReturnsStatusCode()
        {
            // ARRANGE


            var sut = _fixture.Create<HomeController>();
            var weather = _fixture.Create<Task<WeatherViewModel>>();
           
            WeatherViewModel weatherSubmit = new WeatherViewModel()
            {
                city = "test",
                country = "test",
                zip = ""

            };

            Mock<IWeatherService> weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(x => x.GetWeather(It.IsAny<WeatherViewModel>()))
              .Returns(weather);



            // ACT
            var result = await sut.Index(weatherSubmit);


            // ASSERT
            Xunit.Assert.NotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.RedirectResult));

        }
    }
}
