using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoryDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Models;
using RealEstate_Dapper_UI.Services;
using System.Text;

namespace RealEstate_Dapper_UI.Areas.EstateAgent.Controllers
{
    [Area("EstateAgent")]
    [Authorize] // uses the signed-in user id (GetUserID); anonymous requests used to throw NullReferenceException
    public class MyAdvertsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginServices _loginServices;
        private readonly ApiSettings _apiSettings;

        public MyAdvertsController(
            IHttpClientFactory httpClientFactory,
            ILoginServices loginServices,
            IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _loginServices = loginServices;
            _apiSettings = apiSettings.Value;
        }

        // ================= ACTIVE =================
        public async Task<IActionResult> ActiveAdverts()
        {
            var userId = _loginServices.GetUserID;

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var response = await client.GetAsync(
                "Products/GetProductAdvertsListByEmployeeAsyncByTrue?id=" + userId);

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultProductAdvertsListWithCategoryByEmployeeDtos>());

            var json = await response.Content.ReadAsStringAsync();
            var values =
                JsonConvert.DeserializeObject<List<ResultProductAdvertsListWithCategoryByEmployeeDtos>>(json);

            return View(values ?? new List<ResultProductAdvertsListWithCategoryByEmployeeDtos>());
        }

        // ================= PASSIVE =================
        public async Task<IActionResult> PassiveAdverts()
        {
            var userId = _loginServices.GetUserID;

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var response = await client.GetAsync(
                "Products/GetProductAdvertsListByEmployeeAsyncByFalse?id=" + userId);

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultProductAdvertsListWithCategoryByEmployeeDtos>());

            var json = await response.Content.ReadAsStringAsync();
            var values =
                JsonConvert.DeserializeObject<List<ResultProductAdvertsListWithCategoryByEmployeeDtos>>(json);

            return View(values ?? new List<ResultProductAdvertsListWithCategoryByEmployeeDtos>());
        }

        // ================= CREATE =================
        [HttpGet]
        public async Task<IActionResult> CreateAdvert()
        {
            await FillCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvert(CreateProductDtos model)
        {
            model.DealOfTheDay = false;
            model.AdvertisementDate = DateTime.Now;
            model.ProductStatus = true;

            var userId = _loginServices.GetUserID;
            model.AppUserID = int.Parse(userId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("Products", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("ActiveAdverts");

            await FillCategories();
            return View(model);
        }

        // ================= TURN OFF =================
        public async Task<IActionResult> ProductAdsPageStatusChangeToFalse(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            await client.GetAsync($"Products/ProductAdsPageStatusChangeToFalse/{id}");

            return RedirectToAction("ActiveAdverts");
        }

        // ================= TURN ON (ACTIVATE) =================
        public async Task<IActionResult> ProductAdsPageStatusChangeToTrue(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            await client.GetAsync($"Products/ProductAdsPageStatusChangeToTrue/{id}");

            return RedirectToAction("PassiveAdverts");
        }

        // ================= DELETE =================
        public async Task<IActionResult> DeleteAdvert(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            await client.DeleteAsync($"Products/{id}");

            return RedirectToAction("ActiveAdverts");
        }

        // ================= HELPER =================
        private async Task FillCategories()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var response = await client.GetAsync("Categories");
            var json = await response.Content.ReadAsStringAsync();
            var values = (response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<List<ResultCategoryDtos>>(json) : null)
                ?? new List<ResultCategoryDtos>();

            ViewBag.v = values.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryID.ToString()
            }).ToList();
        }
    }
}
