using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.AmenitiesDtos;
using RealEstate_Dapper_UI.Models;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    [Authorize]
    public class AmenityController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public AmenityController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync("Amenities");

            if (!responseMessage.IsSuccessStatusCode)
                return View(new List<ResultAllAmenitiesDtos>());

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultAllAmenitiesDtos>>(jsonData);

            return View(values ?? new List<ResultAllAmenitiesDtos>());
        }
        [HttpGet]
        public IActionResult CreateAmenity()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAmenity(CreateAmenityDtos createAmenityDtos)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var jsonData = JsonConvert.SerializeObject(createAmenityDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var reponseMessage = await client.PostAsync("Amenities", stringContent);
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.DeleteAsync($"Amenities/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["DashboardError"] = "The action could not be completed because the API returned an error. Please try again.";
            return RedirectToAction("Index"); // this action has no view; a bare View() threw "view not found"
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAmenity(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"Amenities/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateAmenityDtos>(jsondata);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAmenity(UpdateAmenityDtos updateAmenityDtos)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var jsonData = JsonConvert.SerializeObject(updateAmenityDtos);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var reponseMessage = await client.PutAsync("Amenities", stringContent);
            if (reponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
