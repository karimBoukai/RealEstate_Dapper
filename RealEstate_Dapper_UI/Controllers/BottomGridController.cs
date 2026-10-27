using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.BottomGridDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class BottomGridController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BottomGridController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44387/api/BottomGrids");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBottomGridDtos>>(jsondata);
                return View(values);
            }

            return View();
        }
        public IActionResult CreateBottomGrid()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBottomGrid(CreateBottomGridDtos createBottomGridDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBottomGridDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var reponseMessage = await client.PostAsync("https://localhost:44387/api/BottomGrids", stringContent);
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteBottomGrid(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44387/api/BottomGrids/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["DashboardError"] = "The action could not be completed because the API returned an error. Please try again.";
            return RedirectToAction("Index"); // this action has no view; a bare View() threw "view not found"
        }
        [HttpGet]
        public async Task<IActionResult> UpdateBottomGrid(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44387/api/BottomGrids/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBottomGridDtos>(jsondata);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBottomGrid(UpdateBottomGridDtos updateBottomGridDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBottomGridDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var reponseMessage = await client.PutAsync("https://localhost:44387/api/BottomGrids", stringContent);
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
