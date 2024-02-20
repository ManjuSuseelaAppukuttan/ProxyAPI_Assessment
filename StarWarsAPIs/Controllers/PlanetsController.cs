using APICommons;
using APIInterfaces;
using APIModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace StarWarsAPIs.Controllers
{
    [ApiController]
    [Route("starwarsapi/planets")]
    [Tags("Planets")]
    public class PlanetsController : ControllerBase
    {
        #region Fields
        private readonly ILogger<PlanetsController> _logger;
        private readonly IStarWarsAPIService<PlanetsModel> _starWarsAPIService;
      
        #endregion

        #region Constructor
        public PlanetsController(ILogger<PlanetsController> logger,
                              IStarWarsAPIService<PlanetsModel> starWarsService)
        {
            _logger = logger;
            _starWarsAPIService = starWarsService;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// This method is used to retreive all star wars planets details
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Success</response>
        /// <response code="204">No contect</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<PlanetsViewResponseModel>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetAllPlanets()
        {
            try
            {
                (HttpStatusCode, List<PlanetsModel>?) result = await _starWarsAPIService.GetAll(APIConstants.PlanetsApiPath);
                if (result.Item1 == HttpStatusCode.OK && result.Item2 != null)
                {
                    List<PlanetsViewResponseModel> viewModels = result.Item2.Select(item => item.Map()).ToList();//result.Item2.Select(item => _mapper.Map<PlanetsViewResponseModel>(item)).ToList();
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
        ///This method is used to retreive the star wars planets details based on the given id
        /// </summary>
        /// <remarks></remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(PlanetsViewResponseModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetPlanetById(int id)
        {
            try
            {
                (HttpStatusCode, PlanetsModel?) result = await _starWarsAPIService.GetById(APIConstants.PlanetsApiPath, id);
                if (result.Item1 == HttpStatusCode.OK && result.Item2 != null)
                {
                    PlanetsViewResponseModel viewModel = result.Item2.Map();//_mapper.Map<PlanetsViewResponseModel>(result.Item2);
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
        /// This method is used to retreive the star wars planets details based on the given list of ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("GetByIds")]
        [ProducesResponseType(typeof(PlanetsViewResponseModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetAllPlanetsByIds([FromBody] int[] ids)
        {
            try
            {
                var response = await _starWarsAPIService.GetByIds(APIConstants.PlanetsApiPath, ids);

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
