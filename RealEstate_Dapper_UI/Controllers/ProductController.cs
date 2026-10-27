using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoryDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Models;

namespace RealEstate_Dapper_UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public ProductController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
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

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Categories");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            // null when the Categories call fails — keep the form usable instead of throwing
            var values = (responseMessage.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<ResultCategoryDtos>>(jsonData) : null)
                         ?? new List<ResultCategoryDtos>();
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.v = categoryValues;
            return View();
        }

        public async Task<IActionResult> ProductAdsPageStatusChangeToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"Products/ProductAdsPageStatusChangeToFalse/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["DashboardError"] = "The action could not be completed because the API returned an error. Please try again.";
            return RedirectToAction("Index"); // this action has no view; a bare View() threw "view not found"
        }
        public async Task<IActionResult> ProductAdsPageStatusChangeToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync($"Products/ProductAdsPageStatusChangeToTrue/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            TempData["DashboardError"] = "The action could not be completed because the API returned an error. Please try again.";
            return RedirectToAction("Index"); // this action has no view; a bare View() threw "view not found"
        }

    }
}