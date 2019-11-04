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

namespace UnitTestsWeatherAPI
{
    [TestClass]
    public abstract class UnitTest1
    {
        protected abstract Task SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
        [Fact]
        public async void TestMethod1()
        {
            // ARRANGE
            Mock<IDataService> dataServiceMock = new Mock<IDataService>();

            dataServiceMock.Setup(x => x.GetDataAsString(It.IsAny<string>()))
                .ReturnsAsync("Your json");

            var subjectUnderTest = new WeatherService(dataServiceMock.Object);

            WeatherViewModel weatherViewModel = new WeatherViewModel()
            {
                city = "test",
                country = "test",
                zip = "74737"

            };

            // ACT
            var result = await subjectUnderTest
                   .GetTemperature(weatherViewModel);

            // ASSERT
            // result.Should().NotBeNull(); // this is fluent assertions here...
            // result.city.Should().Be(1);

        }
    }
}
