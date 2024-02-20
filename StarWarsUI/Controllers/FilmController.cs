using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StarWarsUI.Models;
using System.Text;

namespace StarWarsUI.Controllers
{
    public class FilmController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5017/starwarsapi/films");

            List<FilmViewModel> filmList = new List<FilmViewModel>();
            var response = await httpClient.GetAsync(httpClient.BaseAddress + "/GetAll").Result.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(response))
            {
                filmList = JsonConvert.DeserializeObject<List<FilmViewModel>>(response);
            }
            return View(filmList);
        }       
    }
}
