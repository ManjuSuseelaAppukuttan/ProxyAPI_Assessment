using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StarWarsUI.Models;
using System.Text;

namespace StarWarsUI.Controllers
{
    public class StarshipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Starships(List<string> starships)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5017/starwarsapi/starships");
            var json = JsonConvert.SerializeObject(starships.Select(int.Parse).ToArray());
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            List<StarshipViewModel> planetList = new List<StarshipViewModel>();
            var response = await httpClient.PostAsync(httpClient.BaseAddress + "/GetByIds", httpContent).Result.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(response))
            {
                planetList = JsonConvert.DeserializeObject<List<StarshipViewModel>>(response);
            }
            return View(planetList);
        }
    }
}
