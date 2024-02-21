using APIInterfaces;
using APIModels.Models;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net;

namespace APIServices
{
    public class StarWarsAPIService<T> : IStarWarsAPIService<T> where T : BaseModel
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly HttpClient _httpClient;

        #endregion

        #region Constructor

        public StarWarsAPIService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("starWarsClient");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method is uesd to retrieve all data from the specified uri with given ids
        /// </summary>
        /// <param name="apiName"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByIds(string apiName, int[] ids)
        {
            var bag = new ConcurrentBag<T>();
            var tasks = ids.Select(async id =>
            {
                var response = await GetById(apiName, id);
                if (response.Item1 == HttpStatusCode.OK && response.Item2 != null)
                {
                    bag.Add(response.Item2);
                }

            });
            await Task.WhenAll(tasks);
            var count = bag.Count;
            return bag.ToList<T>();
        }

        /// <summary>
        ///  This method is uesd to retrieve all data from the specified uri
        /// </summary>
        /// <param name="apiName"></param>
        /// <returns></returns>
        public async Task<(HttpStatusCode, List<T>?)> GetAll(string apiName)
        {
            (HttpStatusCode, string) reponse = await GetHttpResponse(apiName);
            if (reponse.Item1 == HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<ItemsModel<T>>(reponse.Item2);
                return (HttpStatusCode.OK, data?.results);
            }

            return (reponse.Item1, null);
        }

        /// <summary>
        ///  This method is uesd to retrieve the data from the specified uri with given id
        /// </summary>
        /// <param name="apiName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(HttpStatusCode, T?)> GetById(string apiName, int id)
        {
            var reponse = await GetHttpResponse($"{apiName}/{id}");
            if (reponse.Item1 == HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<T>(reponse.Item2);
                return (HttpStatusCode.OK, data);
            }
            return (reponse.Item1, null);
        }

        #endregion

        #region Private methods
        private async Task<(HttpStatusCode, string)> GetHttpResponse(string api)
        {
            using (var response = await _httpClient.GetAsync(api, HttpCompletionOption.ResponseHeadersRead))
            {
                var responseStr = await response.Content.ReadAsStringAsync();
                return (response.StatusCode, responseStr);
            }

        }
        #endregion
    }
}
