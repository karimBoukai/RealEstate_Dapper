using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Models;
using System.Net.Http;

namespace RealEstate_Dapper_UI.Controllers
{
    public class DealOfTheDayController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public DealOfTheDayController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Products/GetProductWithCategory");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDtos>>(jsonData);
                return View(values);

            }

            return View();
        }


        public async Task<IActionResult> ProductDealOfTheDayeStatusChangeToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"Products/ProductDealOfTheDayeStatusChangeToFalse/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["DashboardError"] = "The action could not be completed because the API returned an error. Please try again.";
            return RedirectToAction("Index"); // this action has no view; a bare View() threw "view not found"
        }
        public async Task<IActionResult> ProductDealOfTheDayeStatusChangeToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"Products/ProductDealOfTheDayeStatusChangeToTrue/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["DashboardError"] = "The action could not be completed because the API returned an error. Please try again.";
            return RedirectToAction("Index"); // this action has no view; a bare View() threw "view not found"
        }
    }
}
