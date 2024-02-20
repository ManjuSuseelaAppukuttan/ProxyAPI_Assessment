using APIModels.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace StarWarsUnitTests.Service
{
    public class StarWarsAPIServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientMock;
        public StarWarsAPIServiceTests()
        {
            _httpClientMock = new Mock<IHttpClientFactory>();
        }

        [Fact]
        public async Task GetAllFilms_Ok()
        {
            // Arrange
            var api = "films";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new ItemsModel<FilmModel> { results = new List<FilmModel> { new FilmModel() } }))
            };

            var _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                  ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                  ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(_httpMessageHandler.Object);

            httpClient.BaseAddress = new Uri("http://nonexisting.domain");

            _httpClientMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var starWarService = new APIServices.StarWarsAPIService<FilmModel>(_httpClientMock.Object);

            // Act
            var result = await starWarService.GetAll(api);

            // Assert
            Xunit.Assert.Equal(HttpStatusCode.OK, result.Item1);
            Xunit.Assert.NotNull(result.Item2);
            Xunit.Assert.Single(result.Item2);
        }
    }
}
