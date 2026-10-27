using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealEstate_Dapper_UI.Models;
using System.Net.Http;

namespace RealEstate_Dapper_UI.ViewComponents.Dashboard
{
    public class _DashboardStatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _DashboardStatisticsComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            #region Statistic1
            var client1 = _httpClientFactory.CreateClient();
            client1.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage1 = client1.GetAsync("Statistics/ProductCount");
            var jsonData1 = await responseMessage1.Result.Content.ReadAsStringAsync();
            ViewBag.ProductCount = jsonData1;
            #endregion Statistic1   



            #region Static 2 
            var client2 = _httpClientFactory.CreateClient();
            client2.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage2 = client2.GetAsync("Statistics/EmployeeNameByMaxProductCount");
            var jsonData2 = await responseMessage2.Result.Content.ReadAsStringAsync();
            ViewBag.EmployeeNameByMaxProductCount = jsonData2;
            #endregion static2

            #region Static 3
            var client3 = _httpClientFactory.CreateClient();
            client3.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage3 = client3.GetAsync("Statistics/DifferentCityCount");
            var jsonData3 = await responseMessage3.Result.Content.ReadAsStringAsync();
            ViewBag.DifferentCityCount = jsonData3;
            #endregion static3

            #region Static 4
            var client4 = _httpClientFactory.CreateClient();
            client4 .BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage4 = client4.GetAsync("Statistics/AvgProductPriceByRent");
            var jsonData4 = await responseMessage4.Result.Content.ReadAsStringAsync();
            ViewBag.AvgProductPriceByRent = jsonData4;
            #endregion static4






            return View();  
        }
    }

}
