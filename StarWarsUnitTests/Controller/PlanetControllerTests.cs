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
    public class PlanetsControllerTests
    {
        private Mock<IStarWarsAPIService<PlanetsModel>> _starWarsAPIServiceMock;
        private PlanetsController _planetsController;
        private Mock<ILogger<PlanetsController>> _mockLogger;

        public PlanetsControllerTests()
        {
            _starWarsAPIServiceMock = new Mock<IStarWarsAPIService<PlanetsModel>>();
            _mockLogger = new Mock<ILogger<PlanetsController>>();
            _planetsController = new PlanetsController(_mockLogger.Object, _starWarsAPIServiceMock.Object);            
        }


        [Fact]
        public async Task GetAllPlanets_Ok()
        {
            // Arrange
            var planetsModel = new List<PlanetsModel>() { new PlanetsModel { Name = "Planet1", Films = new List<string> { }  } };
            _starWarsAPIServiceMock.Setup(s => s.GetAll(APIConstants.PlanetsApiPath)).ReturnsAsync((HttpStatusCode.OK, planetsModel));

            // Act
            var result = await _planetsController.GetAllPlanets();

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPlanetById_Ok()
        {
            // Arrange
            var filmId = 1;
            var planetsModel = new PlanetsModel { Name = "Planet1", Films = new List<string> { } };
            _starWarsAPIServiceMock.Setup(s => s.GetById(APIConstants.PlanetsApiPath, filmId)).ReturnsAsync((HttpStatusCode.OK, planetsModel));

            // Act
            var result = await _planetsController.GetPlanetById(filmId);

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPlanetById_NotFound()
        {
            // Arrange
            var planetId = 1;
            _starWarsAPIServiceMock.Setup(s => s.GetById(APIConstants.PlanetsApiPath, planetId)).ReturnsAsync((HttpStatusCode.NotFound, (PlanetsModel?)null));

            // Act
            var result = await _planetsController.GetPlanetById(planetId);

            // Assert
            Xunit.Assert.IsType<StatusCodeResult>(result);
            var objectResult = (StatusCodeResult)result;
            Xunit.Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }
    }
}
