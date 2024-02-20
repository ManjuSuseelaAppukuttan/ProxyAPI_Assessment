﻿using APICommons;
using APIInterfaces;
using APIModels.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace StarWarsAPIs.Controllers
{

    [ApiController]
    [Route("starwarsapi/films")]
    [Tags("Flims")]
    public class FilmController : ControllerBase
    {
        #region Fields
        private readonly ILogger<FilmController> _logger;
        private readonly IStarWarsAPIService<FilmModel> _starWarsAPIService;
        #endregion

        #region Constructor
        public FilmController(ILogger<FilmController> logger,
                              IStarWarsAPIService<FilmModel> starWarsService)
        {
            _logger = logger;
            _starWarsAPIService = starWarsService;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// This method is used to retreive all star wars films details
        /// </summary>
        /// <remarks></remarks>
        /// <response code="200">Success</response>
        /// <response code="204">No contect</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("GetAllFilms")]
        [ProducesResponseType(typeof(List<FilmViewResponseModel>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetAllFilms()
        {
            try
            {
                (HttpStatusCode, List<FilmModel>?) result = await _starWarsAPIService.GetAllByName(APIConstants.FilmApiPath);
                if (result.Item1 == HttpStatusCode.OK && result.Item2 != null)
                {
                    List<FilmViewResponseModel> viewModels = result.Item2.Select(item => item.Map()).ToList();//result.Item2.Select(item => _mapper.Map<FilmViewResponseModel>(item)).ToList();
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
        /// This method is used to retreive the star wars films details based on the given id
        /// </summary>
        /// <remarks>Api for particular film details</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("GetFilmById")]       
        [ProducesResponseType(typeof(FilmViewResponseModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetFilmById(int id)
        {
            try
            {
                (HttpStatusCode, FilmModel?) result = await _starWarsAPIService.GetByNameAndId(APIConstants.FilmApiPath, id);
                if (result.Item1 == HttpStatusCode.OK && result.Item2 != null)
                {
                    FilmViewResponseModel viewModel = result.Item2.Map(); //_mapper.Map<FilmViewResponseModel>(result.Item2);
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
        ///  This method is used to retreive the star wars planets details based on the given list of ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("GetAllFilmsByIds")]
        [ProducesResponseType(typeof(PlanetsViewResponseModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetAllFilmsByIds([FromBody] int[] ids)
        {
            try
            {
                var response = await _starWarsAPIService.GetAllByIds(APIConstants.FilmApiPath, ids);

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
