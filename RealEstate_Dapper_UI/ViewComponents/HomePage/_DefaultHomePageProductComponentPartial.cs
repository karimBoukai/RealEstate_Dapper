using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Models;

namespace RealEstate_Dapper_UI.ViewComponents.HomePage
{
    public class _DefaultHomePageProductComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _DefaultHomePageProductComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync("Products/GetProductByDealOfTheDayTrueWithCategory");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDtos>>(jsondata);

                return View("~/Views/Shared/Components/_DefaultHomePageProductComponentPartial/Default.cshtml",
                            values ?? new List<ResultProductDtos>());
            }

            return View("~/Views/Shared/Components/_DefaultHomePageProductComponentPartial/Default.cshtml",
                        new List<ResultProductDtos>());
        }
    }
}
