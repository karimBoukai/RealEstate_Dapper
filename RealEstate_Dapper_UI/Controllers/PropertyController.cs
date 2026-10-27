using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.ProductDetailsDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Models;

namespace RealEstate_Dapper_UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public PropertyController(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Ads()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync("Products/GetActiveProductForAdsPage");

            if (!responseMessage.IsSuccessStatusCode)
                return View("Index", new List<GetProductsForAdsPageDtos>()); // there is no Ads.cshtml

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<GetProductsForAdsPageDtos>>(jsonData);

            return View("Index", values ?? new List<GetProductsForAdsPageDtos>());
        }


        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync("Products/GetProductWithCategory");

            // Views/Property/Index.cshtml is typed List<GetProductsForAdsPageDtos>; passing
            // List<ResultProductDtos> threw InvalidOperationException. Same API call, mapped field-for-field.
            var list = new List<GetProductsForAdsPageDtos>();
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDtos>>(jsonData) ?? new List<ResultProductDtos>();
                list = values.Select(x => new GetProductsForAdsPageDtos
                {
                    ProductID = x.ProductID,
                    Title = x.Title,
                    Price = x.Price,
                    City = x.City,
                    District = x.District,
                    Address = x.Address,
                    Type = x.Type,
                    CategoryName = x.CategoryName,
                    ProductStatus = x.ProductStatus,
                    CoverImage = x.CoverImage,
                    SlugUrl = x.SlugUrl
                }).ToList();
            }

            return View(list);
        }
        public async Task<IActionResult> PropertyListWithSearch(string searchKeyValue, int propertyCategoryID, string city)
        {
            searchKeyValue = TempData["searchKeyValue"]?.ToString() ?? searchKeyValue ?? "";
            city = TempData["city"]?.ToString() ?? city ?? "";

            if (!int.TryParse(TempData["propertyCategoryID"]?.ToString(), out var catId))
                catId = propertyCategoryID;

            propertyCategoryID = catId;

            ViewBag.searchKeyValue = searchKeyValue;
            ViewBag.propertyCategoryID = propertyCategoryID;
            ViewBag.city = city;

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync(
                $"Products/ResultProductWithSearchList?searchKeyValue={searchKeyValue}&propertyCategoryID={propertyCategoryID}&city={city}"
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithSearchListDtos>>(jsonData);
                return View(values ?? new List<ResultProductWithSearchListDtos>());
            }

            return View(new List<ResultProductWithSearchListDtos>());
        }

        [HttpGet("property/{slug}/{id}")]
        public async Task<IActionResult> PropertySingle(string slug, int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync("Products/GetProductByProductID?id=" + id);
            if (!responseMessage.IsSuccessStatusCode)
                return NotFound();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<ResultProductDtos>(jsonData);
            if (values == null)
                return NotFound();

            var correctSlug = string.IsNullOrWhiteSpace(values.SlugUrl)
                ? CreateSlug(values.Title)
                : values.SlugUrl;

            if (!string.Equals(slug, correctSlug, StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("PropertySingle", "Property",
                    new { slug = correctSlug, id = id });
            }

            // Load property details
            var client2 = _httpClientFactory.CreateClient();
            client2.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage2 = await client2.GetAsync("ProductDetails/GetProductDetailsByProductID?id=" + id);
            if (!responseMessage2.IsSuccessStatusCode)
                return NotFound();

            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<GetPRoductDetailsByIdDtos>(jsonData2);
            if (values2 == null)
                return NotFound();

            ViewBag.i = id;

            ViewBag.Title1 = values.Title;
            ViewBag.Price = values.Price;
            ViewBag.City = values.City;
            ViewBag.District = values.District;
            ViewBag.Address = values.Address;
            ViewBag.Type = values.Type;
            ViewBag.AdvertisementDate = values.AdvertisementDate;

            var timeSpan = DateTime.Now - values.AdvertisementDate;
            ViewBag.DateDiff = timeSpan.Days / 30;

            ViewBag.Description = values.Description;

            ViewBag.BathCount = values2.bathCount;
            ViewBag.BedRoomCount = values2.bedRoomCount;
            ViewBag.RoomCount = values2.roomCount;
            ViewBag.BuildYear = values2.buildYear;
            ViewBag.GarageSize = values2.garageSize;
            ViewBag.ProductSize = values2.productSize;
            ViewBag.ProductID = values2.productID;
            ViewBag.StructureType = values2.StructureType;
            ViewBag.ExteriorMaterial = values2.ExteriorMaterial;

            ViewBag.SlugUrl = correctSlug;

            return View(values);
        }

        private string CreateSlug(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "";

            title = title.ToLowerInvariant().Trim();
            title = System.Text.RegularExpressions.Regex.Replace(title, @"[^a-z0-9\s-]", "");
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s+", " ").Trim();
            title = title.Replace(" ", "-");
            title = System.Text.RegularExpressions.Regex.Replace(title, "-{2,}", "-");

            return title;
        }
    }
}
