using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StarWarsUI.Models;
using System.Text;

namespace StarWarsUI.Controllers
{
    public class PlanetsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Planet(List<string> planets)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5017/starwarsapi/planets");
            var json = JsonConvert.SerializeObject(planets.Select(int.Parse).ToArray());
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            List<PlanetViewModel> planetList = new List<PlanetViewModel>();
            var response = await httpClient.PostAsync(httpClient.BaseAddress + "/GetByIds", httpContent).Result.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(response))
            {
                planetList = JsonConvert.DeserializeObject<List<PlanetViewModel>>(response);
            }
            return View(planetList);
        }
    }
}
