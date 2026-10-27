using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealEstate_Dapper_UI.Models;

namespace RealEstate_Dapper_UI.ViewComponents.EstateAgent
{
    public class _EstateAgentDashboardChartComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettings _apiSettings;

        public _EstateAgentDashboardChartComponentPartial(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            var responseMessage = await client.GetAsync("Chart");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dtos.EstateAgentDtos.EstateAgentDashboardChartDtos>>(jsonData);
                return View(values ?? new List<Dtos.EstateAgentDtos.EstateAgentDashboardChartDtos>());
            }
            return View(new List<Dtos.EstateAgentDtos.EstateAgentDashboardChartDtos>()); // never a bare View(): it would inherit the parent page model
        }
    }
}