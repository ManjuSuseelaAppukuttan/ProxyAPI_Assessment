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
    public class FilmControllerTests
    {
        private Mock<IStarWarsAPIService<FilmModel>> _starWarsAPIServiceMock;
        private FilmController _filmController;
        private Mock<ILogger<FilmController>> _mockLogger;

        public FilmControllerTests()
        {
            _starWarsAPIServiceMock = new Mock<IStarWarsAPIService<FilmModel>>();
            _mockLogger = new Mock<ILogger<FilmController>>();
            _filmController = new FilmController(_mockLogger.Object, _starWarsAPIServiceMock.Object);
        }

        [Fact]
        public async Task GetAllFilms_Ok()
        {
            // Arrange
            var filmModels = new List<FilmModel>() { new FilmModel { Title = "Film1", Planets = new List<string> { }, Starships = new List<string> { } } };
            _starWarsAPIServiceMock.Setup(s => s.GetAll(APIConstants.FilmApiPath)).ReturnsAsync((HttpStatusCode.OK, filmModels));

            // Act
            var result = await _filmController.GetAllFilms();

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFilmById_Ok()
        {
            // Arrange
            var filmId = 1;
            var filmModel = new FilmModel { Title = "Film1", Planets = new List<string> { } , Starships = new List<string>() };
            _starWarsAPIServiceMock.Setup(s => s.GetById(APIConstants.FilmApiPath, filmId)).ReturnsAsync((HttpStatusCode.OK, filmModel));

            // Act
            var result = await _filmController.GetFilmById(filmId);

            // Assert 
            Xunit.Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFilmById_NotFound()
        {
            // Arrange
            var filmId = 1;
            _starWarsAPIServiceMock.Setup(s => s.GetById(APIConstants.FilmApiPath, filmId)).ReturnsAsync((HttpStatusCode.NotFound, (FilmModel?)null));

            // Act
            var result = await _filmController.GetFilmById(filmId);

            // Assert
            Xunit.Assert.IsType<StatusCodeResult>(result);
            var objectResult = (StatusCodeResult)result;
            Xunit.Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

    }
}
