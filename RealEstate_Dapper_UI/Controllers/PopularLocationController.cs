using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.PopularLocationsDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class PopularLocationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PopularLocationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44387/api/PopularLocations");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPopularLocationDtos>>(jsondata);
                return View(values);
            }

            return View();
        }

        public IActionResult CreatePopularLocations()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePopularLocations(CreatePopularLocationDtos createPopularLocations)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createPopularLocations);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(
                "https://localhost:44387/api/PopularLocations",
                stringContent
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> DeletePopularLocations(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync(
                $"https://localhost:44387/api/PopularLocations/{id}"
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            TempData["DashboardError"] = "The action could not be completed because the API returned an error. Please try again.";
            return RedirectToAction("Index"); // this action has no view; a bare View() threw "view not found"
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePopularLocations(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(
                $"https://localhost:44387/api/PopularLocations/{id}"
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdatePopularLocationDtos>(jsondata);
                return View(values);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePopularLocations(UpdatePopularLocationDtos updatePopularLocationDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updatePopularLocationDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync(
                "https://localhost:44387/api/PopularLocations",
                stringContent
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
