using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_UI.Services;

namespace RealEstate_Dapper_UI.ViewComponents.EstateAgent
{
    public class _EstateAgentDashboardStatisticComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginServices _loginServices;

        public _EstateAgentDashboardStatisticComponentPartial(IHttpClientFactory httpClientFactory, ILoginServices loginServices)
        {
            _httpClientFactory = httpClientFactory;
            _loginServices = loginServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = _loginServices.GetUserID;

            var client = _httpClientFactory.CreateClient();

            #region Statistic1
            var responseMessage1 = await client.GetAsync("https://localhost:44387/api/EstateAgentDashboardStatistic/AllProductCount");
            if (responseMessage1.IsSuccessStatusCode)
            {
                var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
                ViewBag.AllProductCount = jsonData1;
            }
            #endregion

            #region Statistic2
            var responseMessage2 = await client.GetAsync($"https://localhost:44387/api/EstateAgentDashboardStatistic/ProductCountByEmployeeID?id={id}");
            if (responseMessage2.IsSuccessStatusCode)
            {
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                ViewBag.EmployeeByProductCount = jsonData2;
            }
            #endregion

            #region Statistic3
            var responseMessage3 = await client.GetAsync($"https://localhost:44387/api/EstateAgentDashboardStatistic/ProductCountByStatusTrue?id={id}");
            if (responseMessage3.IsSuccessStatusCode)
            {
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                ViewBag.ProductCountByStatusTrue = jsonData3;
            }
            #endregion

            #region Statistic4
            var responseMessage4 = await client.GetAsync($"https://localhost:44387/api/EstateAgentDashboardStatistic/ProductCountByStatusFalse?id={id}");
            if (responseMessage4.IsSuccessStatusCode)
            {
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                ViewBag.ProductCountByStatusFalse = jsonData4;
            }
            #endregion

            return View();
        }
    }
}
