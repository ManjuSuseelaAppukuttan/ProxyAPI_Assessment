using APICommons;
using APIInterfaces;
using APIModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StarWarsAPIs.Controllers;
using System.Net;
using Xunit;

namespace StarWarsUnitTests.Controller
{
    public class StarshipControllerTests
    {

        private Mock<IStarWarsAPIService<StarshipModel>> _starWarsAPIServiceMock;
        private StarshipController _startshipController;
        private Mock<ILogger<StarshipController>> _mockLogger;
        public StarshipControllerTests()
        {
            _starWarsAPIServiceMock = new Mock<IStarWarsAPIService<StarshipModel>>();
            _mockLogger = new Mock<ILogger<StarshipController>>();
            _startshipController = new StarshipController(_mockLogger.Object, _starWarsAPIServiceMock.Object);

        }

        [Fact]
        public async Task GetAllStarships_Ok()
        {
            // Arrange
            var starshipModel = new List<StarshipModel>() { new StarshipModel { Name = "startship1", Films = new List<string> { } } };
            _starWarsAPIServiceMock.Setup(s => s.GetAll(APIConstants.StarshipApiPath)).ReturnsAsync((HttpStatusCode.OK, starshipModel));

            // Act
            var result = await _startshipController.GetAllStarships();

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetStartshipById_Ok()
        {
            // Arrange
            var id = 1;
            var starshipModel = new StarshipModel { Name = "startship1", Films = new List<string> { } };
            _starWarsAPIServiceMock.Setup(s => s.GetById(APIConstants.StarshipApiPath, id)).ReturnsAsync((HttpStatusCode.OK, starshipModel));

            // Act
            var result = await _startshipController.GetStarshipById(id);

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetStartshipById_NotFound()
        {
            // Arrange
            var id = 1;
            _starWarsAPIServiceMock.Setup(s => s.GetById(APIConstants.StarshipApiPath, id)).ReturnsAsync((HttpStatusCode.NotFound, (StarshipModel?)null));

            // Act
            var result = await _startshipController.GetStarshipById(id);

            // Assert
            Xunit.Assert.IsType<StatusCodeResult>(result);
            var objectResult = (StatusCodeResult)result;
            Xunit.Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }
    }
}
