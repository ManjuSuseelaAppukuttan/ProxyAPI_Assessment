using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StarWarsUI.Models;

namespace StarWarsUI.Controllers
{
    public class FilmController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5017/starwarsapi/films");
        private readonly HttpClient _httpClient;

        public FilmController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<FilmViewModel> filmList = new List<FilmViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/GetAll").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                filmList = JsonConvert.DeserializeObject<List<FilmViewModel>>(data);
            }
            return View(filmList);
        }
    }
}
