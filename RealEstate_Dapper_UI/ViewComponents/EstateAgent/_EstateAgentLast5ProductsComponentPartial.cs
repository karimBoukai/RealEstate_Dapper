using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealEstate_Dapper_UI.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Models;
using RealEstate_Dapper_UI.Services;

namespace RealEstate_Dapper_UI.ViewComponents.EstateAgent
{
    public class _EstateAgentLast5ProductsComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginServices _loginServices;
        private readonly ApiSettings _apiSettings;

        public _EstateAgentLast5ProductsComponentPartial(IHttpClientFactory httpClientFactory, ILoginServices loginServices, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _loginServices = loginServices;
            _apiSettings = apiSettings.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = _loginServices.GetUserID;
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);

            var responseMessage = await client.GetAsync("EstateAgentLastProduct?id="+ id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = Newtonsoft.Json.JsonConvert.DeserializeObject<List< ResultLast5ProductWithCategoryDtos>> (jsonData);
                return View(values ?? new List<ResultLast5ProductWithCategoryDtos>());
            }
            return View(new List<ResultLast5ProductWithCategoryDtos>()); // never a bare View(): it would inherit the parent page model
        }
    }
}
