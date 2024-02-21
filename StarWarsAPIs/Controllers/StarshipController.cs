using APICommons;
using APIInterfaces;
using APIModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Net;

namespace StarWarsAPIs.Controllers
{
    [ApiController]
    [Route("starwarsapi/starships")]
    [Tags("Starship")]
    [EnableRateLimiting("fixed")]
    public class StarshipController : ControllerBase
    {
        #region Fields
        private readonly ILogger<StarshipController> _logger;
        private readonly IStarWarsAPIService<StarshipModel> _starWarsAPIService;
        #endregion

        #region Constructor
        public StarshipController(ILogger<StarshipController> logger,
                              IStarWarsAPIService<StarshipModel> starWarsService)
        {
            _logger = logger;
            _starWarsAPIService = starWarsService;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// This method is used to retrieve all star ships details
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<StarshipViewResponseModel>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetAllStarships()
        {
            try
            {
                (HttpStatusCode, List<StarshipModel>?) result = await _starWarsAPIService.GetAll(APIConstants.StarshipApiPath);
                if (result.Item1 == HttpStatusCode.OK && result.Item2 != null)
                {
                    List<StarshipViewResponseModel> viewModels = result.Item2.Select(film => film.Map()).ToList();
                    return Ok(viewModels);
                }
                return StatusCode((int)result.Item1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                };
                return new ObjectResult(problemDetails)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        /// <summary>
        /// This method is used to retrieve the star ships details based on the given id
        /// </summary>
        /// <remarks></remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(StarshipViewResponseModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetStarshipById(int id)
        {
            try
            {
                (HttpStatusCode, StarshipModel?) result = await _starWarsAPIService.GetById(APIConstants.StarshipApiPath, id);
                if (result.Item1 == HttpStatusCode.OK && result.Item2 != null)
                {
                    StarshipViewResponseModel viewModel = result.Item2.Map();
                    return Ok(viewModel);
                }
                return StatusCode((int)result.Item1);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                };
                return new ObjectResult(problemDetails)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }

        /// <summary>
        ///  This method is used to retrieve the star ships details based on the given list of ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("GetByIds")]
        public async Task<IActionResult> GetAllStarshipsByIds([FromBody] int[] ids)
        {
            try
            {
                var response = await _starWarsAPIService.GetByIds(APIConstants.StarshipApiPath, ids);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                };
                return new ObjectResult(problemDetails)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        #endregion
    }
}
