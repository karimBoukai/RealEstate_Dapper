using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.EmployeeDtos;
using RealEstate_Dapper_UI.Dtos.WhoWeAreDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class WhoWeAreController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WhoWeAreController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:44387/api/WhoWeAreDetail");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDtos>>(jsondata);
                return View(values);
            }

            return View();
        }
        public IActionResult CreateWhoWeAreDetail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhoWeAreDetail(CreateWhoWeAreDtos createWhoWeAreDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createWhoWeAreDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var reponseMessage = await client.PostAsync("https://localhost:44387/api/WhoWeAreDetail", stringContent);
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteWhoWeAreDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:44387/api/WhoWeAreDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["DashboardError"] = "The action could not be completed because the API returned an error. Please try again.";
            return RedirectToAction("Index"); // this action has no view; a bare View() threw "view not found"
        }
        [HttpGet]
        public async Task<IActionResult> UpdateWhoWeAreDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:44387/api/WhoWeAreDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateWhoWeAreDtos>(jsondata);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateWhoWeAreDetail(UpdateWhoWeAreDtos updateWhoWeAreDtos)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateWhoWeAreDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var reponseMessage = await client.PutAsync("https://localhost:44387/api/WhoWeAreDetail", stringContent);
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
